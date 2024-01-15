using e_Estoque_API.Core.Events.Taxes;

namespace e_Estoque_API.Core.Entities
{
    public class Tax : AggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Percentage { get; set; }

        #region EFCRelations

        public Guid IdCategory { get; set; }
        public virtual Category Category { get; set; } = null!;

        #endregion EFCRelations
        public Tax()
        {
            
        }

        public Tax(Guid id, string name, string description, decimal percentage, Guid idCategory)
        {
            Id = id;
            Name = name;
            Description = description;
            Percentage = percentage;
            IdCategory = idCategory;
        }

        public static Tax Create(string name, string description, decimal percentage, Guid idCategory)
        {
            var tax = new Tax(Guid.NewGuid(), name, description, percentage, idCategory);

            tax.CreatedAt = DateTime.UtcNow;

            tax.AddEvent(new TaxCreated(tax.Id, tax.Name, tax.Description, tax.Percentage, tax.IdCategory));

            return tax;
        }

        public void Update(string name, string description, decimal percentage, Guid idCategory)
        {
            Name = name;
            Description = description;
            Percentage = percentage;
            IdCategory = idCategory;

            UpdatedAt = DateTime.UtcNow;

            AddEvent(new TaxUpdated(Id, Name, Description, Percentage, IdCategory));
        }
    }
}
