namespace e_Estoque_API.Core.Events.Taxes
{
    public class TaxUpdated : IDomainEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Percentage { get; private set; }
        public Guid IdCategory { get; set; }

        public TaxUpdated(Guid id, string name, string description, decimal percentage, Guid idCategory)
        {
            Id = id;
            Name = name;
            Description = description;
            Percentage = percentage;
            IdCategory = idCategory;
        }
    }
}
