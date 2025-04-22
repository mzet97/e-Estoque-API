using e_Estoque_API.Core.Enums;
using e_Estoque_API.Core.Events.Sales;
using e_Estoque_API.Core.Validations;

namespace e_Estoque_API.Core.Entities;

public class Sale : AggregateRoot
{
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal TotalTax { get; private set; }

    public SaleType SaleType { get; private set; }
    public PaymentType PaymentType { get; private set; }

    public DateTime? DeliveryDate { get; private set; }
    public DateTime SaleDate { get; private set; }
    public DateTime? PaymentDate { get; private set; }

    #region EFCRelations

    public Guid IdCustomer { get; private set; }
    public Customer Customer { get; private set; } = null!;

    public virtual IEnumerable<SaleProduct> SaleProducts { get; private set; } = null!;

    #endregion EFCRelations

    public Sale()
    {
    }


    protected Sale(
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
        IEnumerable<SaleProduct> saleProducts,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        bool isDeleted
        ) : base(
            id,
            createdAt,
            updatedAt,
            deletedAt,
            isDeleted)
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
        SaleProducts = saleProducts;
    }

    public static Sale Create(
        int quantity,
        decimal totalPrice,
        decimal totalTax,
        SaleType saleType,
        PaymentType paymentType,
        DateTime? deliveryDate,
        DateTime saleDate,
        DateTime? paymentDate,
        Guid idCustomer,
        List<SaleProduct> saleProducts)
    {
        var sale = new Sale(
            Guid.NewGuid(),
            quantity,
            totalPrice,
            totalTax,
            saleType,
            paymentType,
            deliveryDate,
            saleDate,
            paymentDate,
            idCustomer,
            saleProducts,
            DateTime.Now,
            null,
            null,
            false);

        sale.AddEvent(new SaleCreated(
            sale.Id,
            sale.Quantity,
            sale.TotalPrice,
            sale.TotalTax,
            sale.SaleType,
            sale.PaymentType,
            sale.DeliveryDate,
            sale.SaleDate,
            sale.PaymentDate,
            sale.IdCustomer,
            SaleProductsToProduct(sale.SaleProducts)));

        sale.Validate();

        return sale;
    }

    public void Update(
        int quantity,
        decimal totalPrice,
        decimal totalTax,
        SaleType saleType,
        PaymentType paymentType,
        DateTime? deliveryDate,
        DateTime saleDate,
        DateTime? paymentDate,
        Guid idCustomer,
        List<SaleProduct> saleProducts)
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
        SaleProducts = saleProducts;

        Update();

        AddEvent(new SaleUpdated(
            Id,
            Quantity,
            TotalPrice,
            TotalTax,
            SaleType,
            PaymentType,
            DeliveryDate,
            SaleDate,
            PaymentDate,
            IdCustomer,
            SaleProductsToProduct(SaleProducts)));

        Validate();
    }

    public static IEnumerable<Product> SaleProductsToProduct(IEnumerable<SaleProduct> saleProducts)
    {
        return saleProducts.Select(saleProduct => saleProduct.Product);
    }

    public override void Validate()
    {
        var validator = new SaleValidation();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            _errors = result.Errors.Select(x => x.ErrorMessage);
            _isValid = false;
        }
        else
        {
            _errors = Enumerable.Empty<string>();
            _isValid = true;
        }
    }
}