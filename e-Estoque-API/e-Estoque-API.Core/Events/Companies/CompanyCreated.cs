using e_Estoque_API.Core.Entities;

namespace e_Estoque_API.Core.Events.Companies;

public class CompanyCreated : IDomainEvent
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string DocId { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public string PhoneNumber { get; private set; }

    public Guid IdCompanyAddress { get; private set; }
    public Address CompanyAddress { get; private set; }

    public CompanyCreated(
        Guid id,
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        Guid idCompanyAddress,
        Address companyAddress)
    {
        Id = id;
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        IdCompanyAddress = idCompanyAddress;
        CompanyAddress = companyAddress;
    }
}