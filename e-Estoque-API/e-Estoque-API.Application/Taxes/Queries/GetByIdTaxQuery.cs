using e_Estoque_API.Application.Taxes.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Taxes.Queries;

public class GetByIdTaxQuery : IRequest<BaseResult<TaxViewModel>>
{
    public Guid Id { get; set; }

    public GetByIdTaxQuery(Guid id)
    {
        Id = id;
    }
}