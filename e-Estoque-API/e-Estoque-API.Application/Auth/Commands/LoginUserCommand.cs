using e_Estoque_API.Application.Auth.ViewModels;
using MediatR;

namespace e_Estoque_API.Application.Auth.Commands
{
    public class LoginUserCommand : IRequest<TokenViewModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
