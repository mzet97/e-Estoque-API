using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;

namespace e_Estoque_API.API.Configuration
{
    public static class KeycloakConfig
    {
        public static IServiceCollection AddKeycloakConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationOptions = configuration
                            .GetSection(KeycloakAuthenticationOptions.Section)
                            .Get<KeycloakAuthenticationOptions>();

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
    }
}
