using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Enums;

namespace e_Estoque_API.Core.Events.Sales;

public class SaleUpdated : IDomainEvent
{
    public Guid Id { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal TotalTax { get; private set; }

    public SaleType SaleType { get; private set; }
    public PaymentType PaymentType { get; private set; }

    public DateTime? DeliveryDate { get; private set; }
    public DateTime SaleDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }

    public Guid IdCustomer { get; private set; }

    public IEnumerable<Product> Products { get; private set; }

    public SaleUpdated(Guid id,
                       int quantity,
                       decimal totalPrice,
                       decimal totalTax,
                       SaleType saleType,
                       PaymentType paymentType,
                       DateTime? deliveryDate,
                       DateTime saleDate,
                       DateTime? paymentDate,
                       Guid idCustomer,
                       IEnumerable<Product> products)
    {
        Id = id;
        Quantity = quantity;
        TotalPrice = totalPrice;
        TotalTax = totalTax;
        SaleType = saleType;
        PaymentType = paymentType;
        DeliveryDate = deliveryDate;
        SaleDate = saleDate;
        PaymentDate = paymentDate;
        IdCustomer = idCustomer;
        Products = products;
    }
}