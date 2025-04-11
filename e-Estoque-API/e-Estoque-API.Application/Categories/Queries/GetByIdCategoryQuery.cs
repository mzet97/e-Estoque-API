using e_Estoque_API.Application.Categories.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Categories.Queries;

public class GetByIdCategoryQuery : IRequest<BaseResult<CategoryViewModel>>
{
    public Guid Id { get; set; }

    public GetByIdCategoryQuery(Guid id)
    {
        Id = id;
    }
}