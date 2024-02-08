using e_Estoque_API.Application.Auth.ViewModels;
using MediatR;

namespace e_Estoque_API.Application.Auth.Commands
{
    public class SystemLoginCommand : IRequest<TokenViewModel>
    {
    }
}
