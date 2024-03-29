﻿using e_Estoque_API.Core.Enums;
using e_Estoque_API.Core.Events.Sales;

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

    public Sale(
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
    }

    public Sale(
       Guid id,
       int quantity,
       decimal totalPrice,
       decimal totalTax,
       SaleType saleType,
       PaymentType paymentType,
       DateTime? deliveryDate,
       DateTime saleDate,
       DateTime? paymentDate,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
       Guid idCustomer,
       List<SaleProduct> saleProducts)
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
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
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
        quantity,
        totalPrice,
        totalTax,
        saleType,
        paymentType,
        deliveryDate,
        saleDate,
        paymentDate,
        idCustomer,
        saleProducts);

        sale.CreatedAt = DateTime.UtcNow;

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

        UpdatedAt = DateTime.UtcNow;

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
    }

    public static IEnumerable<Product> SaleProductsToProduct(IEnumerable<SaleProduct> saleProducts)
    {
        return saleProducts.Select(saleProduct => saleProduct.Product);
    }
}