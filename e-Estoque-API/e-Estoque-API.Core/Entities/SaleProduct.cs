namespace e_Estoque_API.Core.Entities
{
    public class SaleProduct : AggregateRoot
    {
        public int Quantity { get; private set; }

        public Guid IdProduct { get; private set; }
        public virtual Product Product { get; private set; } = null!;

        public Guid IdSale { get;  private set; }
        public virtual Sale Sale { get; private set; } = null!;


        public SaleProduct()
        {

        }

        public SaleProduct(Guid idProduct, int quantity)
        {
            IdProduct = idProduct;
            Quantity = quantity;
        }

        public SaleProduct(Guid idProduct, Guid idSale, int quantity)
        {
            IdProduct = idProduct;
            IdSale = idSale;
            Quantity = quantity;
        }

        public SaleProduct(Guid id, Guid idProduct, Guid idSale, int quantity)
        {
            Id = id;
            IdProduct = idProduct;
            IdSale = idSale;
            Quantity = quantity;
        }

        public SaleProduct(int quantity, Guid idProduct, Product product, Guid idSale, Sale sale)
        {
            Quantity = quantity;
            IdProduct = idProduct;
            Product = product;
            IdSale = idSale;
            Sale = sale;
        }
    }
}
