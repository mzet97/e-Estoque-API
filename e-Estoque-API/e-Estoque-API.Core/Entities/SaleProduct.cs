using e_Estoque_API.Domain.Entities;
using e_Estoque_API.Domain.Validations;

namespace e_Estoque_API.Core.Entities;

public class SaleProduct : Entity
{

    public int Quantity { get; private set; }

    public Guid IdProduct { get; private set; }
    public virtual Product Product { get; private set; } = null!;

    public Guid IdSale { get; private set; }
    public virtual Sale Sale { get; private set; } = null!;

    public SaleProduct()
    {
    }

    public SaleProduct(
        Guid id,
        int quantity,
        Guid idProduct,
        Guid idSale,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        bool isDeleted) : base(
            id,
            createdAt,
            updatedAt,
            deletedAt,
            isDeleted)
    {
        Quantity = quantity;
        IdProduct = idProduct;
        IdSale = idSale;
    }

    public override void Validate()
    {
        var validator = new SaleProductValidation();
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