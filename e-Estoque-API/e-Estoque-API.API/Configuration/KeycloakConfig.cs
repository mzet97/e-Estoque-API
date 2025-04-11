using Keycloak.AuthServices.Authentication;

namespace e_Estoque_API.API.Configuration;

public static class KeycloakConfig
{
    public static IServiceCollection AddKeycloakConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var keycloakSection = configuration.GetSection("Keycloak");
        if (!keycloakSection.Exists())
            throw new ArgumentException("A seção 'Keycloak' não foi encontrada no appsettings.");

        var keycloak = GetKeycloak(keycloakSection);
        services.AddSingleton(keycloak);

        services.AddKeycloakWebApiAuthentication(
            keycloakSection,
            options =>
            {
                options.RequireHttpsMetadata = false;
            });

        return services;
    }

    private static e_Estoque_API.Core.Models.Keycloak GetKeycloak(IConfigurationSection keycloakSection)
    {
        var keycloak = new e_Estoque_API.Core.Models.Keycloak
        {
            Realm = keycloakSection["realm"] ?? "",
            AuthServerUrl = keycloakSection["auth-server-url"] ?? "",
            SslRequired = keycloakSection["ssl-required"] ?? "",
            Resource = keycloakSection["resource"] ?? "",
            VerifyTokenAudience = keycloakSection["verify-token-audience"] ?? "",
            Credentials = new e_Estoque_API.Core.Models.Credentials
            {
                Secret = keycloakSection["credentials:secret"] ?? "",
            },
            ConfidentialPort = keycloakSection["confidential-port"] ?? ""
        };

        ValidateKeys(keycloak);
        return keycloak;
    }

    private static void ValidateKeys(e_Estoque_API.Core.Models.Keycloak keycloak)
    {
        if (string.IsNullOrEmpty(keycloak.Realm))
            throw new ArgumentException("Keycloak:Realm é obrigatório.");
        if (string.IsNullOrEmpty(keycloak.AuthServerUrl))
            throw new ArgumentException("Keycloak:AuthServerUrl é obrigatório.");
        if (string.IsNullOrEmpty(keycloak.SslRequired))
            throw new ArgumentException("Keycloak:SslRequired é obrigatório.");
        if (string.IsNullOrEmpty(keycloak.Resource))
            throw new ArgumentException("Keycloak:Resource é obrigatório.");
        if (string.IsNullOrEmpty(keycloak.VerifyTokenAudience))
            throw new ArgumentException("Keycloak:VerifyTokenAudience é obrigatório.");
        if (string.IsNullOrEmpty(keycloak.Credentials.Secret))
            throw new ArgumentException("Keycloak:Credentials:Secret é obrigatório.");
        if (string.IsNullOrEmpty(keycloak.ConfidentialPort))
            throw new ArgumentException("Keycloak:ConfidentialPort é obrigatório.");
    }
}
