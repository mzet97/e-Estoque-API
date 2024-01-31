using e_Estoque_API.Application.Auth.ViewModels;
using MediatR;

namespace e_Estoque_API.Application.Auth.Commands
{
    public class RegisterUserCommand : IRequest<TokenViewModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Token { get; set; }

    }
}
