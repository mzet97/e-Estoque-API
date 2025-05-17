using e_Estoque_API.Core.Enums;
using MediatR;

namespace e_Estoque_API.Application.Sales.Commands;

public class CreateSaleCommand : IRequest<Guid>
{
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TotalTax { get; set; }

    public SaleType SaleType { get; set; }
    public PaymentType PaymentType { get; set; }

    public DateTime? DeliveryDate { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public Guid IdCustomer { get; set; }

    public IEnumerable<Guid> IdsProducts { get; set; }
}
