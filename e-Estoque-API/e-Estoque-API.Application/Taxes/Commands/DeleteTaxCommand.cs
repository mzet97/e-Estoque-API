using MediatR;

namespace e_Estoque_API.Application.Taxes.Commands;

public class DeleteTaxCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public DeleteTaxCommand(Guid id)
    {
        Id = id;
    }
}