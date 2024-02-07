namespace e_Estoque_API.Core.Events.Products;

public class ProductUpdated : IDomainEvent
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ShortDescription { get; private set; }
    public decimal Price { get; private set; }
    public decimal Weight { get; private set; }
    public decimal Height { get; private set; }
    public decimal Length { get; private set; }
    public Guid IdCategory { get; private set; }
    public Guid IdCompany { get; private set; }

    public ProductUpdated(
        Guid id,
        string name,
        string description,
        string shortDescription,
        decimal price,
        decimal weight,
        decimal height,
        decimal length,
        string image,
        Guid idCategory,
        Guid idCompany)
    {
        Id = id;
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
        Price = price;
        Weight = weight;
        Height = height;
        Length = length;
        IdCategory = idCategory;
        IdCompany = idCompany;
    }
}