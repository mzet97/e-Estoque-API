using e_Estoque_API.Domain.Events;

namespace e_Estoque_API.Core.Events.Taxes;

public class TaxCreated : DomainEvent
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Percentage { get; private set; }
    public Guid IdCategory { get; set; }

    public TaxCreated(
        Guid id,
        string name,
        string description,
        decimal percentage,
        Guid idCategory) : base()
    {
        Id = id;
        Name = name;
        Description = description;
        Percentage = percentage;
        IdCategory = idCategory;
    }
}