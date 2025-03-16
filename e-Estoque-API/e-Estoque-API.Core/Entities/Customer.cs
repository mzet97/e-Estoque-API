using e_Estoque_API.Core.Events.Customers;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Domain.Entities;
using e_Estoque_API.Domain.ValueObjects;

namespace e_Estoque_API.Core.Entities;

public class Customer : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string DocId { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public CustomerAddress CustomerAddress { get; private set; } = null!;

    public Customer()
    {
    }

    public Customer(
        Guid id,
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CustomerAddress customerAddress,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        bool isDeleted
        ) : base(id, createdAt, updatedAt, deletedAt, isDeleted)
    {
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        CustomerAddress = customerAddress;
    }

    public static Customer Create(
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CustomerAddress customerAddress)
    {
        var company = new Customer(
            Guid.NewGuid(),
            name,
            docId,
            email,
            description,
            phoneNumber,
            customerAddress,
            DateTime.Now,
            null,
            null,
            false);


        company.AddEvent(new CustomerCreated(
            company.Id,
            company.Name,
            company.DocId,
            company.Email,
            company.Description,
            company.PhoneNumber,
            company.CustomerAddress));

        return company;
    }

    public void Update(
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CustomerAddress customerAddress)
    {
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        CustomerAddress = customerAddress;

        Update();

        AddEvent(new CustomerUpdated(
            Id,
            Name,
            DocId,
            Email,
            Description,
            PhoneNumber,
            customerAddress));
    }

    public override void Validate()
    {
        var validator = new CustomerValidation();
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