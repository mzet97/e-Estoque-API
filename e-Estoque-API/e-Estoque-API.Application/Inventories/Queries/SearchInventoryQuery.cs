using e_Estoque_API.Application.Dtos.InputModels;
using e_Estoque_API.Application.Inventories.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Inventories.Queries;

public class SearchInventoryQuery : BaseSearch, IRequest<BaseResultList<InventoryViewModel>>
{
    public int Quantity { get; set; }
    public DateTime DateOrder { get; set; }

    public Guid IdProduct { get; set; }
}