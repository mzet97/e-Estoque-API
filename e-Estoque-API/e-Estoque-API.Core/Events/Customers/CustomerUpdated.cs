using e_Estoque_API.Domain.Events;
using e_Estoque_API.Domain.ValueObjects;

namespace e_Estoque_API.Core.Events.Customers;

public class CustomerUpdated : DomainEvent
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string DocId { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public string PhoneNumber { get; private set; }

    public CustomerAddress CustomerAddress { get; private set; }

    public CustomerUpdated(
        Guid id,
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CustomerAddress customerAddress) : base()
    {
        Id = id;
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        CustomerAddress = customerAddress;
    }
}