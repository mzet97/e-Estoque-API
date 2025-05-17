using e_Estoque_API.Application.Customers.ViewModels;
using e_Estoque_API.Application.Dtos.ViewModels;
using e_Estoque_API.Application.Products.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Enums;

namespace e_Estoque_API.Application.Sales.ViewModels;

public class SaleViewModel : BaseViewModel
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
    public CustomerViewModel Customer { get; set; }

    public IEnumerable<ProductViewModel> Products { get; set; }

    public SaleViewModel(
        Guid id,
        int quantity,
        decimal totalPrice,
        decimal totalTax,
        SaleType saleType,
        PaymentType paymentType,
        DateTime? deliveryDate,
        DateTime saleDate,
        DateTime? paymentDate,
        Guid idCustomer,
        CustomerViewModel customer,
        IEnumerable<ProductViewModel> products,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt
    ) : base(
        id,
        createdAt,
        updatedAt,
        deletedAt)
    {
        Quantity = quantity;
        TotalPrice = totalPrice;
        TotalTax = totalTax;
        SaleType = saleType;
        PaymentType = paymentType;
        DeliveryDate = deliveryDate;
        SaleDate = saleDate;
        PaymentDate = paymentDate;
        IdCustomer = idCustomer;
        Customer = customer;
        Products = products;
    }

    public static SaleViewModel FromEntity(Sale entity)
    {
        return new SaleViewModel(
            entity.Id,
            entity.Quantity,
            entity.TotalPrice,
            entity.TotalTax,
            entity.SaleType,
            entity.PaymentType,
            entity.DeliveryDate,
            entity.SaleDate,
            entity.PaymentDate,
            entity.IdCustomer,
            entity.Customer != null ? CustomerViewModel.FromEntity(entity.Customer) : null,
            entity.SaleProducts.Select(x => ProductViewModel.FromEntity(x.Product)),
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt);
    }
}