using e_Estoque_API.Domain.ValueObjects;
using MediatR;

namespace e_Estoque_API.Application.Companies.Commands;

public class UpdateCompanyCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DocId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public CompanyAddress CompanyAddress { get; set; }

}