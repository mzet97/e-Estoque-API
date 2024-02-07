using e_Estoque_API.Core.Events.Customers;

namespace e_Estoque_API.Core.Entities
{
    public class Customer : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public string DocId { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public Guid IdCustomerAddress { get; private set; }
        public virtual CustomerAddress CustomerAddress { get; private set; } = null!;

        public Customer()
        {
        }

        public Customer(Guid id, string name, string docId, string email, string description, string phoneNumber, Guid idCustomerAddress, CustomerAddress customerAddress)
        {
            Id = id;
            Name = name;
            DocId = docId;
            Email = email;
            Description = description;
            PhoneNumber = phoneNumber;
            IdCustomerAddress = idCustomerAddress;
            CustomerAddress = customerAddress;
        }

        public static Customer Create(string name, string docId, string email, string description, string phoneNumber, CustomerAddress customerAddress)
        {
            var company = new Customer(Guid.NewGuid(), name, docId, email, description, phoneNumber, Guid.NewGuid(), customerAddress);

            company.CreatedAt = DateTime.UtcNow;

            company.AddEvent(new CustomerCreated(company.Id, company.Name, company.DocId, company.Email, company.Description, company.PhoneNumber, company.IdCustomerAddress, company.CustomerAddress));

            return company;
        }

        public void Update(string name, string docId, string email, string description, string phoneNumber, Guid idCustomerAddress, CustomerAddress customerAddress)
        {
            Name = name;
            DocId = docId;
            Email = email;
            Description = description;
            PhoneNumber = phoneNumber;
            IdCustomerAddress = idCustomerAddress;
            CustomerAddress = customerAddress;

            UpdatedAt = DateTime.UtcNow;

            AddEvent(new CustomerUpdated(Id, Name, DocId, Email, Description, PhoneNumber, idCustomerAddress, customerAddress));
        }
    }
}
