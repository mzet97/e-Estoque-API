using e_Estoque_API.Core.Events.Inventories;

namespace e_Estoque_API.Core.Entities
{
    public class Inventory : AggregateRoot
    {
        public int Quantity { get; set; }
        public DateTime DateOrder { get; set; }

        #region EFCRelations

        public Guid IdProduct { get; set; }
        public virtual Product Product { get; set; } = null!;

        #endregion EFCRelations

        public Inventory(Guid id, int quantity, DateTime dateOrder, Guid idProduct)
        {
            Id = id;
            Quantity = quantity;
            DateOrder = dateOrder;
            IdProduct = idProduct;
        }

        public static Inventory Create(int quantity, DateTime dateOrder, Guid idProduct)
        {
            var inventory = new Inventory(Guid.NewGuid(), quantity, dateOrder, idProduct);

            inventory.CreatedAt = DateTime.UtcNow;

            inventory.AddEvent(new InventoryCreated(inventory.Id, inventory.Quantity, inventory.DateOrder, inventory.IdProduct));

            return inventory;
        }

        public void Update(int quantity, DateTime dateOrder, Guid idProduct)
        {
            Quantity = quantity;
            DateOrder = dateOrder;
            IdProduct = idProduct;

            UpdatedAt = DateTime.UtcNow;

            AddEvent(new InventoryUpdated(Id, Quantity, DateOrder, IdProduct));
        }
    }
}
