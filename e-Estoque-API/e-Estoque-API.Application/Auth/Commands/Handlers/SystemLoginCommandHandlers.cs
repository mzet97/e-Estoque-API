using e_Estoque_API.Application.Auth.ViewModels;
using e_Estoque_API.Application.Common.Behaviours;
using e_Estoque_API.Core.Models;
using MediatR;
using System.Text.Json;

namespace e_Estoque_API.Application.Auth.Commands.Handlers
{
    public class SystemLoginCommandHandlers : IRequestHandler<SystemLoginCommand, TokenViewModel>
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Keycloak _keycloak;

        public SystemLoginCommandHandlers(IHttpClientFactory clientFactory, Keycloak keycloak)
        {
            _clientFactory = clientFactory;
            _keycloak = keycloak;
        }

        public async Task<TokenViewModel> Handle(SystemLoginCommand request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient();
            var url = $"{_keycloak.AuthServerUrl}realms/{_keycloak.Realm}/protocol/openid-connect/token";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            var formurlencoded = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _keycloak.Resource },
                { "client_secret", _keycloak.Credentials.Secret },
            };
            httpRequest.Content = new FormUrlEncodedContent(formurlencoded);
            var response = await client.SendAsync(httpRequest).Result.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = true
            };

            var token = JsonSerializer.Deserialize<TokenViewModel>(response, serializeOptions);

            if (token == null)
                throw new ForbiddenAccessException("Invalid username or password");

            return token;
        }
    }
}
