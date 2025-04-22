using e_Estoque_API.Application.Companies.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Companies.Queries;

public class GetByIdCompanyQuery : IRequest<BaseResult<CompanyViewModel>>
{
    public Guid Id { get; set; }

    public GetByIdCompanyQuery(Guid id)
    {
        Id = id;
    }
}