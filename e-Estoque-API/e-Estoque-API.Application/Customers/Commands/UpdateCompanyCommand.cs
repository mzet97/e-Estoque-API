using e_Estoque_API.Application.Dtos.InputModels;
using MediatR;

namespace e_Estoque_API.Application.Customers.Commands;

public class UpdateCustomerCommand : IRequest<Guid>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string DocId { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;

    public Guid IdCustomerAddress { get; set; }
    public AddressInputModel Address { get; set; } = null!;
}