using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace e_Estoque_API.API.Configuration
{
    public static class KeycloakConfig
    {
        public static IServiceCollection AddKeycloakConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationOptions = configuration
                            .GetSection(KeycloakAuthenticationOptions.Section)
                            .Get<KeycloakAuthenticationOptions>();

            var keycloakSection = configuration.GetSection("Keycloak");
            var keycloak = GetKeycloak(keycloakSection);

            services.AddSingleton(keycloak);

            services.AddKeycloakAuthentication(authenticationOptions);

            var authorizationOptions = configuration
                            .GetSection(KeycloakProtectionClientOptions.Section)
                            .Get<KeycloakProtectionClientOptions>();

            services.AddKeycloakAuthorization(authorizationOptions);

            var adminClientOptions = configuration
                            .GetSection(KeycloakAdminClientOptions.Section)
                            .Get<KeycloakAdminClientOptions>();

            services.AddKeycloakAdminHttpClient(adminClientOptions);


            return services;
        }

        private static Core.Models.Keycloak GetKeycloak(IConfigurationSection keycloakSection)
        {
            var keycloak = new e_Estoque_API.Core.Models.Keycloak();
            keycloak.Realm = keycloakSection["realm"];
            keycloak.AuthServerUrl = keycloakSection["auth-server-url"];
            keycloak.SslRequired = keycloakSection["ssl-required"];
            keycloak.Resource = keycloakSection["resource"];
            keycloak.VerifyTokenAudience = keycloakSection["verify-token-audience"];
            keycloak.Credentials = new e_Estoque_API.Core.Models.Credentials();
            keycloak.Credentials.Secret = keycloakSection["credentials:secret"];
            keycloak.ConfidentialPort = keycloakSection["confidential-port"];

            ValidateKeys(keycloak);

            return keycloak;
        }

        private static void ValidateKeys(Core.Models.Keycloak keycloak)
        {
            if (string.IsNullOrEmpty(keycloak.Realm))
                throw new Exception("Keycloak:Realm is required");
            if (string.IsNullOrEmpty(keycloak.AuthServerUrl))
                throw new Exception("Keycloak:AuthServerUrl is required");
            if (string.IsNullOrEmpty(keycloak.SslRequired))
                throw new Exception("Keycloak:SslRequired is required");
            if (string.IsNullOrEmpty(keycloak.Resource))
                throw new Exception("Keycloak:Resource is required");
            if (string.IsNullOrEmpty(keycloak.VerifyTokenAudience))
                throw new Exception("Keycloak:VerifyTokenAudience is required");
            if (string.IsNullOrEmpty(keycloak.Credentials.Secret))
                throw new Exception("Keycloak:Credentials:Secret is required");
            if (string.IsNullOrEmpty(keycloak.ConfidentialPort))
                throw new Exception("Keycloak:ConfidentialPort is required");
        }
    }
}
