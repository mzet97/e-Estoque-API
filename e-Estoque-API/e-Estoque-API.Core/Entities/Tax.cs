using e_Estoque_API.Core.Events.Taxes;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Domain.Entities;

namespace e_Estoque_API.Core.Entities;

public class Tax : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Percentage { get; private set; }

    #region EFCRelations

    public Guid IdCategory { get; private set; }
    public virtual Category Category { get; private set; } = null!;

    #endregion EFCRelations

    public Tax()
    {
    }

    public Tax(
        Guid id,
        string name,
        string description,
        decimal percentage,
        Guid idCategory,
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
        Percentage = percentage;
        IdCategory = idCategory;
    }

    public static Tax Create(
        string name,
        string description,
        decimal percentage,
        Guid idCategory)
    {
        var tax = new Tax(
            Guid.NewGuid(),
            name,
            description,
            percentage,
            idCategory,
            DateTime.Now,
            null,
            null,
            false);
        

        tax.AddEvent(new TaxCreated(
            tax.Id,
            tax.Name,
            tax.Description,
            tax.Percentage,
            tax.IdCategory));

        tax.Validate();

        return tax;
    }

    public void Update(
        string name,
        string description,
        decimal percentage,
        Guid idCategory,
        Category category)
    {
        Name = name;
        Description = description;
        Percentage = percentage;
        IdCategory = idCategory;
        Category = category;
        Update();

        AddEvent(new TaxUpdated(
            Id,
            Name,
            Description,
            Percentage,
            IdCategory));

        Validate();
    }

    public override void Validate()
    {
        var validator = new TaxValidation();
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