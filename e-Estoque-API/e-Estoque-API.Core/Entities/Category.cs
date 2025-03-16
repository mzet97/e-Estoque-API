using e_Estoque_API.Core.Events.Categories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Domain.Entities;

namespace e_Estoque_API.Core.Entities;

public class Category : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string ShortDescription { get; private set; } = string.Empty;

    public virtual IEnumerable<Tax> Taxs { get; private set; } = null!;
    public virtual IEnumerable<Product> Products { get; private set; } = null!;

    public Category()
    {
    }

    public Category(
        Guid id,
        string name,
        string description,
        string shortDescription,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        bool isDeleted
        ) :
        base(id,
             createdAt,
             updatedAt,
             deletedAt,
             isDeleted)
    {
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
        Validate();
    }

    public static Category Create(
        string name,
        string description,
        string shortDescription)
    {
        var category = new Category(
            Guid.NewGuid(),
            name,
            description,
            shortDescription,
            DateTime.Now,
            null,
            null,
            false);

        category.AddEvent(new CategoryCreated(
            category.Id,
            category.Name,
            category.Description,
            category.ShortDescription));

        return category;
    }

    public void Update(
        string name,
        string description,
        string shortDescription)
    {
        Name = name;
        Description = description;
        ShortDescription = shortDescription;

        Update();
        Validate();

        AddEvent(new CategoryUpdated(Id, Name, Description, ShortDescription));
    }

    public override void Validate()
    {
        var validator = new CategoryValidation();
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