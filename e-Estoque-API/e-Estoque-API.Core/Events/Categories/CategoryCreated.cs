using e_Estoque_API.Domain.Events;

namespace e_Estoque_API.Core.Events.Categories;

public class CategoryCreated : DomainEvent
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ShortDescription { get; private set; }

    public CategoryCreated(
        Guid id,
        string name,
        string description,
        string shortDescription) : base()
    {
        Id = id;
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
    }
}