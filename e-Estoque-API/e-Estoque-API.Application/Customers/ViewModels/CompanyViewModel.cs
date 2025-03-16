using e_Estoque_API.Application.Dtos.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Domain.ValueObjects;

namespace e_Estoque_API.Application.Customers.ViewModels;

public class CustomerViewModel : BaseViewModel
{
    public string Name { get; set; }
    public string DocId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public CustomerAddress CustomerAddress { get; set; }

    public CustomerViewModel(
        Guid id,
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CustomerAddress customerAddress,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt
        ) : base(
            id,
            createdAt,
            updatedAt,
            deletedAt)
    {
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        CustomerAddress = customerAddress;
    }

    public static CustomerViewModel FromEntity(Customer entity)
    {
        return new CustomerViewModel(
            entity.Id,
            entity.Name,
            entity.DocId,
            entity.Email,
            entity.Description,
            entity.PhoneNumber,
            entity.CustomerAddress,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt);
    }
}