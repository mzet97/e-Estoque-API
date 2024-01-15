using e_Estoque_API.Core.Events.Products;

namespace e_Estoque_API.Core.Entities
{
    public class Product : AggregateRoot
    {

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }

        public string Image { get; set; } = string.Empty;

        #region EFCRelations

        public Guid IdCategory { get; set; }
        public virtual Category Category { get; set; } = null!;

        public Guid IdCompany { get; set; }
        public virtual Company Company { get; set; } = null!;

        #endregion EFCRelations

        public Product()
        {

        }

        public Product(
            Guid id,
            string name,
            string description,
            string shortDescription,
            decimal price,
            decimal weight,
            decimal height,
            decimal length,
            string image,
            Guid idCategory,
            Guid idCompany)
        {
            Id = id;
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
            Price = price;
            Weight = weight;
            Height = height;
            Length = length;
            Image = image;
            IdCategory = idCategory;
            IdCompany = idCompany;
        }

        public static Product Create(
            string name,
            string description,
            string shortDescription,
            decimal price,
            decimal weight,
            decimal height,
            decimal length,
            string image,
            Guid idCategory,
            Guid idCompany)
        {
            var product = new Product(
                Guid.NewGuid(),
                name,
                description,
                shortDescription,
                price,
                weight,
                height,
                length,
                image,
                idCategory,
                idCompany);

            product.CreatedAt = DateTime.UtcNow;

            product.AddEvent(new ProductCreated(
                product.Id,
                product.Name,
                product.Description,
                product.ShortDescription,
                product.Price,
                product.Weight,
                product.Height,
                product.Length,
                product.IdCategory,
                product.IdCompany));

            return product;
        }

        public void Update(
            string name,
            string description,
            string shortDescription,
            decimal price,
            decimal weight,
            decimal height,
            decimal length,
            string image,
            Guid idCategory,
            Guid idCompany)
        {
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
            Price = price;
            Weight = weight;
            Height = height;
            Length = length;
            Image = image;
            IdCategory = idCategory;
            IdCompany = idCompany;

            UpdatedAt = DateTime.UtcNow;

            AddEvent(new ProductUpdated(
                Id,
                Name,
                Description,
                ShortDescription,
                Price,
                Weight,
                Height,
                Length,
                Image,
                IdCategory,
                IdCompany));
        }
    }
}
