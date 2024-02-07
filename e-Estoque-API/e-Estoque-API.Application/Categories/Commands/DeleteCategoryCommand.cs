using MediatR;

namespace e_Estoque_API.Application.Categories.Commands;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}