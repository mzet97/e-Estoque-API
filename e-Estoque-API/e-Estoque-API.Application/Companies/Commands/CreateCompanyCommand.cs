using e_Estoque_API.Application.Dtos.InputModels;
using MediatR;

namespace e_Estoque_API.Application.Companies.Commands;

public class CreateCompanyCommand : IRequest<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string DocId { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;

    public AddressInputModel Address { get; set; } = null!;
}