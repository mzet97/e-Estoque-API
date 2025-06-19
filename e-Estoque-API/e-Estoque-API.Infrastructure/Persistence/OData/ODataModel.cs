using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Enums;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace e_Estoque_API.Infrastructure.Persistence.OData;

public static class ODataModel
{
    public static IEdmModel Model { get; } = GetEdmModel();

    private static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        builder.EnumType<SaleType>();
        builder.EnumType<PaymentType>();

        var products = builder.EntitySet<Product>("Products");
        products.EntityType.HasKey(p => p.Id);

        products.EntityType.Property(p => p.Name);
        products.EntityType.Property(p => p.Description);
        products.EntityType.Property(p => p.ShortDescription);
        products.EntityType.Property(p => p.Price);
        products.EntityType.Property(p => p.Weight);
        products.EntityType.Property(p => p.Height);
        products.EntityType.Property(p => p.Length);
        products.EntityType.Property(p => p.Image);
        products.EntityType.Property(p => p.IdCategory);
        products.EntityType.Property(p => p.IdCompany);

        products.EntityType.HasRequired(p => p.Category);
        products.EntityType.HasRequired(p => p.Company);

        products.EntityType.Ignore(c => c.Events);

        var categories = builder.EntitySet<Category>("Categories");
        categories.EntityType.HasKey(c => c.Id);

        categories.EntityType.Property(c => c.Name);
        categories.EntityType.Property(c => c.Description);
        categories.EntityType.Property(c => c.ShortDescription);

        categories.EntityType.HasMany(c => c.Taxs);
        categories.EntityType.HasMany(c => c.Products);

        categories.EntityType.Ignore(c => c.Events);

        var companies = builder.EntitySet<Company>("Companies");
        companies.EntityType.HasKey(c => c.Id);

        companies.EntityType.Property(c => c.Name);
        companies.EntityType.Property(c => c.DocId);
        companies.EntityType.Property(c => c.Email);
        companies.EntityType.Property(c => c.Description);
        companies.EntityType.Property(c => c.PhoneNumber);
        companies.EntityType.ComplexProperty(c => c.CompanyAddress);

        companies.EntityType.Ignore(c => c.Events);

        var customers = builder.EntitySet<Customer>("Customers");
        customers.EntityType.HasKey(c => c.Id);

        customers.EntityType.Property(c => c.Name);
        customers.EntityType.Property(c => c.DocId);
        customers.EntityType.Property(c => c.Email);
        customers.EntityType.Property(c => c.Description);
        customers.EntityType.Property(c => c.PhoneNumber);
        customers.EntityType.ComplexProperty(c => c.CustomerAddress);

        customers.EntityType.Ignore(c => c.Events);

        var inventories = builder.EntitySet<Inventory>("Inventories");
        inventories.EntityType.HasKey(c => c.Id);

        inventories.EntityType.Property(c => c.Quantity);
        inventories.EntityType.Property(c => c.DateOrder);
        inventories.EntityType.Property(c => c.IdProduct);

        inventories.EntityType.HasRequired(i => i.Product);

        inventories.EntityType.Ignore(c => c.Events);

        var sales = builder.EntitySet<Sale>("Sales");
        sales.EntityType.HasKey(c => c.Id);

        sales.EntityType.Property(c => c.Quantity);
        sales.EntityType.Property(c => c.TotalPrice);
        sales.EntityType.Property(c => c.TotalTax);
        sales.EntityType.Property(c => c.DeliveryDate);
        sales.EntityType.Property(c => c.SaleDate);
        sales.EntityType.Property(c => c.PaymentDate);
        sales.EntityType.Property(c => c.IdCustomer);

        sales.EntityType.HasRequired(c => c.Customer);

        sales.EntityType.HasMany(c => c.SaleProducts);

        sales.EntityType.Ignore(c => c.Events);

        var saleProducts = builder.EntitySet<SaleProduct>("SaleProducts");
        saleProducts.EntityType.HasKey(c => c.Id);

        saleProducts.EntityType.Property(c => c.Quantity);
        saleProducts.EntityType.Property(c => c.IdProduct);
        saleProducts.EntityType.Property(c => c.IdSale);

        saleProducts.EntityType.HasRequired(c => c.Product);
        saleProducts.EntityType.HasRequired(c => c.Sale);

        saleProducts.EntityType.Ignore(c => c.Events);

        var taxes = builder.EntitySet<Tax>("Taxs");
        taxes.EntityType.HasKey(c => c.Id);

        taxes.EntityType.Property(c => c.Name);
        taxes.EntityType.Property(c => c.Description);
        taxes.EntityType.Property(c => c.IdCategory);

        taxes.EntityType.HasRequired(c => c.Category);

        taxes.EntityType.Ignore(c => c.Events);

        return builder.GetEdmModel();
    }
}