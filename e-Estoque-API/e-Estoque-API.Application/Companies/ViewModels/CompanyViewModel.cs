using e_Estoque_API.Application.Dtos.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Domain.ValueObjects;

namespace e_Estoque_API.Application.Companies.ViewModels;

public class CompanyViewModel : BaseViewModel
{
    public string Name { get; set; }
    public string DocId { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }

    public CompanyAddress CompanyAddress { get; set; }

    public CompanyViewModel(
        Guid id,
        string name,
        string docId,
        string email,
        string description,
        string phoneNumber,
        CompanyAddress companyAddress,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt
        ) : base(id, createdAt, updatedAt, deletedAt)
    {
        Name = name;
        DocId = docId;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        CompanyAddress = companyAddress;
    }

    public static CompanyViewModel FromEntity(Company entity)
    {
        return new CompanyViewModel(
            entity.Id,
            entity.Name,
            entity.DocId,
            entity.Email,
            entity.Description,
            entity.PhoneNumber,
            entity.CompanyAddress,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt);
    }
}