using e_Estoque_API.Application.Auth.ViewModels;
using MediatR;

namespace e_Estoque_API.Application.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<TokenViewModel>
    {
        public string Token { get; set; }
    }
}
