using e_Estoque_API.Core.Events.Products;
using e_Estoque_API.Core.Validations;

namespace e_Estoque_API.Core.Entities;

public class Product : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string ShortDescription { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public decimal Weight { get; private set; }
    public decimal Height { get; private set; }
    public decimal Length { get; private set; }

    public string Image { get; private set; } = string.Empty;

    #region EFCRelations

    public Guid IdCategory { get; private set; }
    public virtual Category Category { get; private set; } = null!;

    public Guid IdCompany { get; private set; }
    public virtual Company Company { get; private set; } = null!;

    #endregion EFCRelations


    public Product()
    {

    }

    protected Product(
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
        Guid idCompany,
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
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
        Price = price;
        Weight = weight;
        Height = height;
        Length = length;
        Image = image;
        IdCategory = idCategory;
        IdCompany = idCompany;
    }

    public static Product Create(
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
        var product = new Product(
            Guid.NewGuid(),
            name,
            description,
            shortDescription,
            price,
            weight,
            height,
            length,
            image,
            idCategory,
            idCompany,
            DateTime.Now,
            null,
            null,
            false);

        product.AddEvent(new ProductCreated(
            product.Id,
            product.Name,
            product.Description,
            product.ShortDescription,
            product.Price,
            product.Weight,
            product.Height,
            product.Length,
            product.IdCategory,
            product.IdCompany));

        product.Validate();

        return product;
    }

    public void Update(
        string name,
        string description,
        string shortDescription,
        decimal price,
        decimal weight,
        decimal height,
        decimal length,
        string image,
        Guid idCategory,
        Category category,
        Guid idCompany,
        Company company)
    {
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
        Price = price;
        Weight = weight;
        Height = height;
        Length = length;
        Image = image;
        IdCategory = idCategory;
        IdCompany = idCompany;
        Category = category;
        Company = company;

        Update();

        AddEvent(new ProductUpdated(
            Id,
            Name,
            Description,
            ShortDescription,
            Price,
            Weight,
            Height,
            Length,
            Image,
            IdCategory,
            IdCompany));

        Validate();
    }

    public override void Validate()
    {
        var validator = new ProductValidation();
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