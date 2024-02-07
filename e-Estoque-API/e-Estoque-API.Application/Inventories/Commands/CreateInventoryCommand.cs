using MediatR;

namespace e_Estoque_API.Application.Inventories.Commands
{
    public class CreateInventoryCommand : IRequest<Guid>
    {
        public int Quantity { get; set; }
        public DateTime DateOrder { get; set; }

        public Guid IdProduct { get; set; }
    }
}
