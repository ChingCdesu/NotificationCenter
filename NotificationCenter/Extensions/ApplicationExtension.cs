using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

    public static void InitializeAuthentications(this WebApplication app)
    {
        var asyncScope = app.Services.CreateAsyncScope();
        var dbContext = asyncScope.ServiceProvider.GetRequiredService<DatabaseContext>();
        
        var schemeProvider = app.Services.GetRequiredService<IAuthenticationSchemeProvider>();
        var oauthOptionsCache = app.Services.GetRequiredService<IOptionsMonitorCache<OAuthOptions>>();

        var enableOAuth =
            Convert.ToBoolean(dbContext.SystemSettings.FirstOrDefault(s => s.Key == "OAuthEnabled")?.Value ?? "false");

        if (enableOAuth)
        {
            schemeProvider.AddScheme(new AuthenticationScheme(
                OAuthDefaults.DisplayName,
                OAuthDefaults.DisplayName,
                typeof(OAuthHandler<>)
            ));
            oauthOptionsCache.TryAdd(OAuthDefaults.DisplayName, new OAuthOptions()
            {
                ClientId = dbContext.SystemSettings.FirstOrDefault(s => s.Key == "OAuthClientId")?.Value ?? string.Empty,
                ClientSecret = dbContext.SystemSettings.FirstOrDefault(s => s.Key == "OAuthClientSecret")?.Value ?? string.Empty,
                CallbackPath = "/oauth/callback",
                AuthorizationEndpoint = dbContext.SystemSettings.FirstOrDefault(s => s.Key == "OAuthAuthorizationEndpoint")?.Value ?? string.Empty,
                TokenEndpoint = dbContext.SystemSettings.FirstOrDefault(s => s.Key == "OAuthTokenEndpoint")?.Value ?? string.Empty,
                UserInformationEndpoint = dbContext.SystemSettings.FirstOrDefault(s => s.Key == "OAuthUserInformationEndpoint")?.Value ?? string.Empty,
                SaveTokens = true,
            });
        }
    }
}