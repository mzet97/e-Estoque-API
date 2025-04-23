using e_Estoque_API.Core.Events.Inventories;
using e_Estoque_API.Core.Validations;
using e_Estoque_API.Domain.Entities;

namespace e_Estoque_API.Core.Entities;

public class Inventory : Entity
{
    public int Quantity { get; private set; }
    public DateTime DateOrder { get; private set; }

    #region EFCRelations

    public Guid IdProduct { get; private set; }
    public virtual Product Product { get; private set; } = null!;

    #endregion EFCRelations

    public Inventory()
    {

    }

    public Inventory(
        Guid id,
        int quantity,
        DateTime dateOrder,
        Guid idProduct,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        bool isDeleted
        ) : base(
            id,
            createdAt,
            updatedAt,
            deletedAt,
            isDeleted)
    {
        Quantity = quantity;
        DateOrder = dateOrder;
        IdProduct = idProduct;
    }

    public static Inventory Create(
        int quantity,
        DateTime dateOrder,
        Guid idProduct)
    {
        var inventory = new Inventory(
            Guid.NewGuid(),
            quantity,
            dateOrder,
            idProduct,
            DateTime.Now,
            null,
            null, 
            false);

        inventory.AddEvent(new InventoryCreated(
            inventory.Id,
            inventory.Quantity,
            inventory.DateOrder,
            inventory.IdProduct));

        inventory.Validate();

        return inventory;
    }

    public void Update(
        int quantity,
        DateTime dateOrder,
        Guid idProduct,
        Product product)
    {
        Quantity = quantity;
        DateOrder = dateOrder;
        IdProduct = idProduct;
        Product = product;

        Update();

        AddEvent(new InventoryUpdated(
            Id,
            Quantity,
            DateOrder,
            IdProduct));

        Validate();
    }

    public override void Validate()
    {
        var validator = new InventoryValidation();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            _errors = result.Errors.Select(x => x.ErrorMessage);
            _isValid = false;
        }
        else
        {
            _errors = Enumerable.Empty<string>();
            _isValid = true;
        }
    }
}