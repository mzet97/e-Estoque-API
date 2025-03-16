using e_Estoque_API.Domain.Events;
using e_Estoque_API.Domain.ValueObjects;

namespace e_Estoque_API.Core.Events.Companies;

public class CompanyUpdated : DomainEvent
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string DocId { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public string PhoneNumber { get; private set; }
    public CompanyAddress CompanyAddress { get; private set; }

    public CompanyUpdated(
        Guid id,
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CompanyAddress companyAddress) : base()
    {
        Id = id;
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        CompanyAddress = companyAddress;
    }
}