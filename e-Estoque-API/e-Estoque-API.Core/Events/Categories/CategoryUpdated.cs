namespace e_Estoque_API.Core.Events.Categories
{
    public class CategoryUpdated : IDomainEvent
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }

        public CategoryUpdated(Guid id, string name, string description, string shortDescription)
        {
            Id = id;
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
        }
    }
}
