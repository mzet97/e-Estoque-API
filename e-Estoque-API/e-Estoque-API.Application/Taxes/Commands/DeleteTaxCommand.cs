using MediatR;

namespace e_Estoque_API.Application.Taxes.Commands;

public class DeleteTaxCommand : IRequest<Unit>
{
    public DeleteTaxCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}