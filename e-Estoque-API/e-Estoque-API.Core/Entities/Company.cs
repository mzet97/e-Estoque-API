using e_Estoque_API.Core.Events.Companies;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Domain.Entities;
using e_Estoque_API.Domain.ValueObjects;

namespace e_Estoque_API.Core.Entities;

public class Company : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string DocId { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public CompanyAddress CompanyAddress { get; private set; } = null!;

    public Company()
    {
    }

    public Company(
        Guid id,
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CompanyAddress companyAddress,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        bool isDeleted
        ) :
        base(
            id,
            createdAt,
            updatedAt,
            deletedAt,
            isDeleted)
    {
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        CompanyAddress = companyAddress;
    }

    public static Company Create(
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CompanyAddress companyAddress)
    {
        var company = new Company(
            Guid.NewGuid(),
            name,
            docId,
            email,
            description,
            phoneNumber,
            companyAddress,
            DateTime.Now,
            null,
            null,
            false
            );


        company.AddEvent(new CompanyCreated(
            company.Id,
            company.Name,
            company.DocId,
            company.Email,
            company.Description,
            company.PhoneNumber,
            company.CompanyAddress));

        return company;
    }

    public void Update(
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CompanyAddress companyAddress)
    {
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        CompanyAddress = companyAddress;

        Update();

        AddEvent(new CompanyUpdated(
            Id,
            Name,
            DocId,
            Email,
            Description,
            PhoneNumber,
            CompanyAddress));
    }

    public override void Validate()
    {
        var validator = new CompanyValidation();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            _errors = result.Errors.Select(x => x.ErrorMessage);
            _isValid = false;
        }
        else
        {
            _errors = Enumerable.Empty<string>();
            _isValid = true;
        }
    }
}