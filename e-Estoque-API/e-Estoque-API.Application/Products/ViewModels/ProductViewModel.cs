using e_Estoque_API.Application.Categories.ViewModels;
using e_Estoque_API.Application.Companies.ViewModels;
using e_Estoque_API.Application.Dtos.ViewModels;
using e_Estoque_API.Core.Entities;

namespace e_Estoque_API.Application.Products.ViewModels;

public class ProductViewModel : BaseViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal Length { get; set; }

    public string Image { get; set; }

    public Guid IdCategory { get; set; }
    public CategoryViewModel Category { get; set; }

    public Guid IdCompany { get; set; }
    public CompanyViewModel Company { get; set; }

    public ProductViewModel(
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
       CategoryViewModel category,
       Guid idCompany,
       CompanyViewModel company,
       DateTime createdAt,
       DateTime? updatedAt,
       DateTime? deletedAt) : base(id, createdAt, updatedAt, deletedAt)
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
        Category = category;
        IdCompany = idCompany;
        Company = company;
    }

    public static ProductViewModel FromEntity(Product entity)
    {
        return new ProductViewModel(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.ShortDescription,
            entity.Price,
            entity.Weight,
            entity.Height,
            entity.Length,
            entity.Image,
            entity.IdCategory,
            entity.Category != null ? CategoryViewModel.FromEntity(entity.Category) : null,
            entity.IdCompany,
            entity.Company != null ? CompanyViewModel.FromEntity(entity.Company) : null,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt);
    }
}