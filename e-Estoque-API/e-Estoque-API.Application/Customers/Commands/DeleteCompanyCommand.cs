using MediatR;

namespace e_Estoque_API.Application.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

     
    }
}
