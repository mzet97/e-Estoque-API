using MediatR;

namespace e_Estoque_API.Application.Taxes.Commands;

public class UpdateTaxCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Percentage { get; set; }

    public Guid IdCategory { get; set; }
}