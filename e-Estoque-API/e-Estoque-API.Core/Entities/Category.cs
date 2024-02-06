using e_Estoque_API.Core.Events.Categories;

namespace e_Estoque_API.Core.Entities
{
    public class Category : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string ShortDescription { get; private set; } = string.Empty;

        public virtual IEnumerable<Tax> Taxs { get; private set; } = null!;
        public virtual IEnumerable<Product> Products { get; private set; } = null!;

        public Category()
        {
            
        }

        public Category(Guid id, string name, string description, string shortDescription)
        {
            Id = id;
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
        }

        public static Category Create(string name, string description, string shortDescription)
        {
            var category = new Category(Guid.NewGuid(), name, description, shortDescription);
            
            category.CreatedAt = DateTime.UtcNow;

            category.AddEvent(new CategoryCreated(category.Id, category.Name, category.Description, category.ShortDescription));

            return category;
        }

        public void Update(string name, string description, string shortDescription)
        {
            Name = name;
            Description = description;
            ShortDescription = shortDescription;

            UpdatedAt = DateTime.UtcNow;

            AddEvent(new CategoryUpdated(Id, Name, Description, ShortDescription));
        }
    }
}
