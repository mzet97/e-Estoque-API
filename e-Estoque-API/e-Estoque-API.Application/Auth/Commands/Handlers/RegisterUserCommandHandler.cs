using e_Estoque_API.Application.Auth.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;
using System.Text;
using System.Text.Json;

namespace e_Estoque_API.Application.Auth.Commands.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenViewModel>
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Keycloak _keycloak;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(IHttpClientFactory clientFactory, Keycloak keycloak, IMediator mediator)
        {
            _clientFactory = clientFactory;
            _keycloak = keycloak;
            _mediator = mediator;
        }

        public async Task<TokenViewModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient();
            var url = $"{_keycloak.AuthServerUrl}admin/realms/{_keycloak.Realm}/users";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);

            var user = new UserDto()
            {
                attributes = new Attributes() { attribute_key = "client" },
                credentials = new Credential[] { new Credential() { temporary = false, type = "password", value = request.Password } },
                username = request.Username,
                firstName = request.FirstName,
                lastName = request.LastName,
                email = request.Email,
                emailVerified = true,
                enabled = true,
            };

            httpRequest.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            httpRequest.Headers.Add("Authorization", $"Bearer {request.Token}");
            var response = await client.SendAsync(httpRequest);

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Error creating user");
            }

            return await _mediator.Send(new LoginUserCommand() { Username = request.Username, Password = request.Password });
        }
    }
}
