using e_Estoque_API.Application.Dtos.ViewModels;
using e_Estoque_API.Core.Entities;

namespace e_Estoque_API.Application.Categories.ViewModels;

public class CategoryViewModel : BaseViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }

    public CategoryViewModel(
        Guid id,
        string name,
        string description,
        string shortDescription,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt
        ) : base(id, createdAt, updatedAt, deletedAt)
    {
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
    }

    public static CategoryViewModel FromEntity(Category entity)
    {
        return new CategoryViewModel(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.ShortDescription,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.DeletedAt);
    }
}