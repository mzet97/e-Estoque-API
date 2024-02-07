using e_Estoque_API.Application.Inventories.ViewModels;
using MediatR;

namespace e_Estoque_API.Application.Inventories.Queries
{
    public class GetByIdInventoryQuery : IRequest<InventoryViewModel>
    {
        public Guid Id { get; set; }

        public GetByIdInventoryQuery(Guid id)
        {
            Id = id;
        }
    }
}
