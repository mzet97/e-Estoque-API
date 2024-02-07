using e_Estoque_API.Application.Dtos.ViewModels;
using e_Estoque_API.Application.Products.ViewModels;
using e_Estoque_API.Core.Entities;

namespace e_Estoque_API.Application.Inventories.ViewModels;

public class InventoryViewModel : BaseViewModel
{
    public int Quantity { get; set; }
    public DateTime DateOrder { get; set; }

    public Guid IdProduct { get; set; }
    public ProductViewModel Product { get; set; }

    public InventoryViewModel(
        Guid id,
        int quantity,
        DateTime dateOrder,
        Guid idProduct,
        ProductViewModel product,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt
        ) : base(id, createdAt, updatedAt, deletedAt)
    {
        Quantity = quantity;
        DateOrder = dateOrder;
        IdProduct = idProduct;
        Product = product;
    }

    public static InventoryViewModel FromEntity(Inventory entity)
    {
        return new InventoryViewModel(
            entity.Id,
            entity.Quantity,
            entity.DateOrder,
            entity.IdProduct,
            ProductViewModel.FromEntity(entity.Product),
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt);
    }
}