using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using NotificationCenter.Authentication;
using NotificationCenter.Storage;
using NotificationCenter.Entities;
using NotificationCenter.Enums;
using NotificationCenter.Utils;

namespace NotificationCenter.Extensions;

public static class ApplicationExtension
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        var asyncScope = app.Services.CreateAsyncScope();
        var dbContext = asyncScope.ServiceProvider.GetRequiredService<DatabaseContext>();
        var logger = asyncScope.ServiceProvider.GetRequiredService<ILogger<DatabaseContext>>();

        var migrations = await dbContext.Database.GetPendingMigrationsAsync();

        if (migrations.Any())
        {
            logger.LogInformation("Applying database migrations...");
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied.");
        }
        else
        {
            logger.LogInformation("Database is up to date.");
        }

        var hasSecuritySalt = await dbContext.SystemSettings.AnyAsync(s => s.Key == "SecuritySalt");
        if (!hasSecuritySalt)
        {
            logger.LogInformation("Creating security salt...");
            var securitySalt = new SystemSetting
            {
                Key = "SecuritySalt",
                Value = RandomUtil.GenerateSalt(),
            };
            await dbContext.SystemSettings.AddAsync(securitySalt);
            await dbContext.SaveChangesAsync();
        }

        var hasAdminRole = await dbContext.Roles.AnyAsync(r => r.Name == "Admin");
        if (!hasAdminRole)
        {
            logger.LogInformation("Creating admin role...");
            var adminRole = new Role
            {
                Name = "Admin",
                SystemRole = true,
                Permissions = (ulong)RolePermission.All,
            };
            await dbContext.Roles.AddAsync(adminRole);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Admin role created.");
        }

        var hasAdminUser = await dbContext.Users.AnyAsync(u => u.Name == "admin");
        if (!hasAdminUser)
        {
            logger.LogInformation("Creating admin user...");
            var securitySalt = await dbContext.SystemSettings.FirstAsync(s => s.Key == "SecuritySalt");
            var password = RandomUtil.GeneratePassword();
            var adminUser = new User
            {
                Name = "admin",
                Password = CryptoUtil.HashPassword(securitySalt + password),
                Roles = new List<Role> { },
            };
            var adminRole = await dbContext.Roles.FirstAsync(r => r.Name == "Admin");
            adminUser.Roles.Add(adminRole);
            await dbContext.Users.AddAsync(adminUser);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Admin user created. Default password is \"{password}\"");
        }
        
        var hasUserRole = await dbContext.Roles.AnyAsync(r => r.Name == "User");
        if (!hasUserRole)
        {
            logger.LogInformation("Creating user role...");
            var userRole = new Role
            {
                Name = "User",
                SystemRole = true,
                Permissions = (ulong)RolePermission.None,
            };
            await dbContext.Roles.AddAsync(userRole);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("User role created.");
        }

        await dbContext.DisposeAsync();
        await asyncScope.DisposeAsync();
    }

    public static void InitializeAuthentications(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration.GetSection("Authentication");
        var authentication = builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });

        authentication.AddCookie();
        authentication.AddSecret();
        
        var googleEnabled = configuration.GetValue("Google:Enabled", false);
        if (googleEnabled)
        {
            var googleClientId = configuration.GetValue("Google:ClientId", string.Empty);
            if (string.IsNullOrEmpty(googleClientId))
            {
                throw new ApplicationException("Google authentication is enabled but client id is not set.");
            }
            var googleClientSecret = configuration.GetValue("Google:ClientSecret", string.Empty);
            if (string.IsNullOrEmpty(googleClientSecret))
            {
                throw new ApplicationException("Google authentication is enabled but client secret is not set.");
            }
            authentication.AddGoogle(options =>
            {
                options.ClientId = googleClientId;
                options.ClientSecret = googleClientSecret;
            });
        }
        
        var oidcEnabled = configuration.GetValue("OpenIdConnect:Enabled", false);
        if (oidcEnabled)
        {
            var oidcAuthority = configuration.GetValue("OpenIdConnect:Authority", string.Empty);
            if (string.IsNullOrEmpty(oidcAuthority))
            {
                throw new ApplicationException("OpenIdConnect authentication is enabled but authority is not set.");
            }
            var oidcAuthorizeEndpoint = configuration.GetValue("OpenIdConnect:AuthorizeEndpoint", string.Empty);
            if (string.IsNullOrEmpty(oidcAuthorizeEndpoint))
            {
                throw new ApplicationException("OpenIdConnect authentication is enabled but authorize endpoint is not set.");
            }
            var oidcTokenEndpoint = configuration.GetValue("OpenIdConnect:TokenEndpoint", string.Empty);
            if (string.IsNullOrEmpty(oidcTokenEndpoint))
            {
                throw new ApplicationException("OpenIdConnect authentication is enabled but token endpoint is not set.");
            }
            var oidcUserInfoEndpoint = configuration.GetValue("OpenIdConnect:UserInfoEndpoint", string.Empty);
            if (string.IsNullOrEmpty(oidcUserInfoEndpoint))
            {
                throw new ApplicationException("OpenIdConnect authentication is enabled but user info endpoint is not set.");
            }
            var oidcClientId = configuration.GetValue("OpenIdConnect:ClientId", string.Empty);
            if (string.IsNullOrEmpty(oidcClientId))
            {
                throw new ApplicationException("OpenIdConnect authentication is enabled but client id is not set.");
            }
            var oidcClientSecret = configuration.GetValue("OpenIdConnect:ClientSecret", string.Empty);
            if (string.IsNullOrEmpty(oidcClientSecret))
            {
                throw new ApplicationException("OpenIdConnect authentication is enabled but client secret is not set.");
            }
            authentication.AddOpenIdConnect("OpenIdConnect", options =>
            {
                options.Authority = oidcAuthority;
                options.ClientId = oidcClientId;
                options.ClientSecret = oidcClientSecret;
                options.Configuration = new OpenIdConnectConfiguration
                {
                    AuthorizationEndpoint = oidcAuthorizeEndpoint,
                    TokenEndpoint = oidcTokenEndpoint,
                    UserInfoEndpoint = oidcUserInfoEndpoint,
                };
                options.CallbackPath = "/oidc/callback";
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.ClaimActions.MapJsonKey("name", "name");
                options.ClaimActions.MapJsonKey("email", "email");
                options.ClaimActions.MapJsonKey("email_verified", "email_verified");
                options.ClaimActions.MapJsonKey("sub", "sub");
                options.ClaimActions.MapJsonKey("preferred_username", "preferred_username");
                options.ClaimActions.MapJsonKey("given_name", "given_name");
                options.ClaimActions.MapJsonKey("family_name", "family_name");
                options.ClaimActions.MapJsonKey("locale", "locale");
                options.ClaimActions.MapJsonKey("picture", "picture");
                options.ClaimActions.MapJsonKey("updated_at", "updated_at");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role",
                };
                
            });
        }
    }
}