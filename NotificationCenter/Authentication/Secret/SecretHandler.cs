using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NotificationCenter.Storage;

namespace NotificationCenter.Authentication.Secret;

public class SecretHandler : AuthenticationHandler<SecretOptions>
{
    private readonly DatabaseContext _database;
    
    [Obsolete("ISystemClock Obsolete")]
    public SecretHandler(DatabaseContext database, IOptionsMonitor<SecretOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
        _database = database;
    }
    
    public SecretHandler(DatabaseContext database, IOptionsMonitor<SecretOptions> options,
        ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
        _database = database;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorization = Request.Headers.Authorization.FirstOrDefault();

        if (string.IsNullOrEmpty(authorization))
        {
            return AuthenticateResult.Fail("Missing authorization header.");
        }
        
        var parts = authorization.Split(' ');
        
        if (parts.Length != 2)
        {
            return AuthenticateResult.Fail("Invalid authorization header.");
        }
        
        var scheme = parts[0];
        if (!scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Invalid authorization scheme.");
        }
        
        var secret = parts[1];
        if (string.IsNullOrEmpty(secret))
        {
            return AuthenticateResult.Fail("Missing authorization secret.");
        }
        
        var existingSecret = await _database.ServiceSecrets
            .Include(s => s.Service)
            .FirstOrDefaultAsync(s => s.HashedServiceSecret == secret);
        
        if (existingSecret is null)
        {
            return AuthenticateResult.Fail("Invalid authorization secret.");
        }

        var claims = new Claim[]
        {
            new("id", existingSecret.Id.ToString()),
            new("serviceId", existingSecret.Service.Id.ToString()),
            new("name", existingSecret.Service.Name)
        };
        
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
