using e_Estoque_API.Domain.Events;

namespace e_Estoque_API.Core.Events.Inventories;

public class InventoryUpdated : DomainEvent
{
    public Guid Id { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DateOrder { get; private set; }
    public Guid IdProduct { get; private set; }

    public InventoryUpdated(
        Guid id,
        int quantity,
        DateTime dateOrder,
        Guid idProduct) : base()
    {
        Id = id;
        Quantity = quantity;
        DateOrder = dateOrder;
        IdProduct = idProduct;
    }
}