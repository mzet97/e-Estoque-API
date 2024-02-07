using MediatR;

namespace e_Estoque_API.Application.Products.Commands;

public class UpdateProductCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Length { get; set; }

    public string Image { get; set; } = string.Empty;

    public Guid IdCategory { get; set; }
    public Guid IdCompany { get; set; }
}