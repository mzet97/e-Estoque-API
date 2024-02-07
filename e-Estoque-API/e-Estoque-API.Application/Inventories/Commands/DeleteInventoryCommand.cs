using MediatR;

namespace e_Estoque_API.Application.Inventories.Commands;

public class DeleteInventoryCommand : IRequest<Unit>
{
    public DeleteInventoryCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}