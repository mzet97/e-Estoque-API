using e_Estoque_API.Application.Products.ViewModels;
using MediatR;

namespace e_Estoque_API.Application.Products.Queries;

public class GetByIdProductQuery : IRequest<ProductViewModel>
{
    public Guid Id { get; set; }

    public GetByIdProductQuery(Guid id)
    {
        Id = id;
    }
}