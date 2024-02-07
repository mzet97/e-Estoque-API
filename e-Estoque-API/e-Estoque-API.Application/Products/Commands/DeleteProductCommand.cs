using MediatR;

namespace e_Estoque_API.Application.Products.Commands;

public class DeleteProductCommand : IRequest<Unit>
{
    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}