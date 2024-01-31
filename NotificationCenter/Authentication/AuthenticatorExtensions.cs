using Microsoft.AspNetCore.Authentication;
using NotificationCenter.Authentication.Secret;

namespace NotificationCenter.Authentication;

public static class AuthenticatorExtensions
{
    public static AuthenticationBuilder AddSecret(this AuthenticationBuilder builder,
        string authenticationSchema = "Secret",
        string? displayName = null,
        Action<SecretOptions>? configureOptions = null)
    {
        builder.AddScheme<SecretOptions, SecretHandler>(
            authenticationSchema,
            displayName, configureOptions);
        return builder;
    }
}