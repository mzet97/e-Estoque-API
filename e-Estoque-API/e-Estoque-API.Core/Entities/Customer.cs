using e_Estoque_API.Core.Events.Customers;

namespace e_Estoque_API.Core.Entities
{
    public class Customer : AggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string DocId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid IdCustomerAddress { get; set; }
        public virtual CustomerAddress CustomerAddress { get; set; } = null!;

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

        public static Customer Create(string name, string docId, string email, string description, string phoneNumber, Guid idCustomerAddress, CustomerAddress customerAddress)
        {
            var company = new Customer(Guid.NewGuid(), name, docId, email, description, phoneNumber, idCustomerAddress, customerAddress);

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
