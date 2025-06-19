using e_Estoque_API.Application.Sales.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Sales.Queries;

public class GetByIdSaleQuery : IRequest<BaseResult<SaleViewModel>>
{
    public Guid Id { get; set; }

    public GetByIdSaleQuery(Guid id)
    {
        Id = id;
    }
}