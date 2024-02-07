using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;

namespace e_Estoque_API.API.Configuration;

public static class KeycloakConfig
{
    public static IServiceCollection AddKeycloakConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var authenticationOptions = configuration
                        .GetSection(KeycloakAuthenticationOptions.Section)
                        .Get<KeycloakAuthenticationOptions>();

        if (authenticationOptions == null)
            throw new ArgumentException("KeycloakAuthenticationOptions is required");

        var keycloakSection = configuration.GetSection("Keycloak");
        var keycloak = GetKeycloak(keycloakSection);

        services.AddSingleton(keycloak);

        services.AddKeycloakAuthentication(authenticationOptions);

        var authorizationOptions = configuration
                        .GetSection(KeycloakProtectionClientOptions.Section)
                        .Get<KeycloakProtectionClientOptions>();

        if (authorizationOptions == null)
            throw new ArgumentException("KeycloakProtectionClientOptions is required");

        services.AddKeycloakAuthorization(authorizationOptions);


        var adminClientOptions = configuration
                        .GetSection(KeycloakAdminClientOptions.Section)
                        .Get<KeycloakAdminClientOptions>();

        if (adminClientOptions == null)
            throw new ArgumentException("KeycloakAdminClientOptions is required");

        services.AddKeycloakAdminHttpClient(adminClientOptions);

        return services;
    }

    private static Core.Models.Keycloak GetKeycloak(IConfigurationSection keycloakSection)
    {
        var keycloak = new e_Estoque_API.Core.Models.Keycloak()
        {
            Realm = keycloakSection["realm"] ?? "",
            AuthServerUrl = keycloakSection["auth-server-url"] ?? "",
            SslRequired = keycloakSection["ssl-required"] ?? "",
            Resource = keycloakSection["resource"] ?? "",
            VerifyTokenAudience = keycloakSection["verify-token-audience"] ?? "",
            Credentials = new e_Estoque_API.Core.Models.Credentials()
            {
                Secret = keycloakSection["credentials:secret"] ?? "",
            },
            ConfidentialPort = keycloakSection["confidential-port"] ?? ""
        };

        ValidateKeys(keycloak);

        return keycloak;
    }

    private static void ValidateKeys(Core.Models.Keycloak keycloak)
    {
        if (string.IsNullOrEmpty(keycloak.Realm))
            throw new ArgumentException("Keycloak:Realm is required");
        if (string.IsNullOrEmpty(keycloak.AuthServerUrl))
            throw new ArgumentException("Keycloak:AuthServerUrl is required");
        if (string.IsNullOrEmpty(keycloak.SslRequired))
            throw new ArgumentException("Keycloak:SslRequired is required");
        if (string.IsNullOrEmpty(keycloak.Resource))
            throw new ArgumentException("Keycloak:Resource is required");
        if (string.IsNullOrEmpty(keycloak.VerifyTokenAudience))
            throw new ArgumentException("Keycloak:VerifyTokenAudience is required");
        if (string.IsNullOrEmpty(keycloak.Credentials.Secret))
            throw new ArgumentException("Keycloak:Credentials:Secret is required");
        if (string.IsNullOrEmpty(keycloak.ConfidentialPort))
            throw new ArgumentException("Keycloak:ConfidentialPort is required");
    }
}