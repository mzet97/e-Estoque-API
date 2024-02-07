using e_Estoque_API.Application.Categories.ViewModels;
using e_Estoque_API.Application.Dtos.ViewModels;
using e_Estoque_API.Core.Entities;

namespace e_Estoque_API.Application.Taxes.ViewModels;

public class TaxViewModel : BaseViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Percentage { get; set; }

    public Guid IdCategory { get; set; }
    public CategoryViewModel Category { get; set; }

    public TaxViewModel(
        Guid id,
        string name,
        string description,
        decimal percentage,
        Guid idCategory,
        CategoryViewModel category,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt) : base(id, createdAt, updatedAt, deletedAt)
    {
        Name = name;
        Description = description;
        Percentage = percentage;
        IdCategory = idCategory;
        Category = category;
    }

    public static TaxViewModel FromEntity(Tax entity)
    {
        return new TaxViewModel(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Percentage,
            entity.IdCategory,
            CategoryViewModel.FromEntity(entity.Category),
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt);
    }
}