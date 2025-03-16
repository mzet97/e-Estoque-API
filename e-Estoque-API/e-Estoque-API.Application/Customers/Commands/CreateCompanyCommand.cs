using e_Estoque_API.Application.Dtos.InputModels;
using e_Estoque_API.Domain.ValueObjects;
using MediatR;

namespace e_Estoque_API.Application.Customers.Commands;

public class CreateCustomerCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string DocId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public CustomerAddress CustomerAddress { get; set; }
}