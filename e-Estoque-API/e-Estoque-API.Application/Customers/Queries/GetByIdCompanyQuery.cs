using e_Estoque_API.Application.Customers.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Customers.Queries;

public class GetByIdCustomerQuery : IRequest<BaseResult<CustomerViewModel>>
{
    public Guid Id { get; set; }

    public GetByIdCustomerQuery(Guid id)
    {
        Id = id;
    }
}