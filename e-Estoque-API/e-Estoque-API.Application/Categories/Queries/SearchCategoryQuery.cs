using e_Estoque_API.Application.Categories.ViewModels;
using e_Estoque_API.Application.Dtos.InputModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Categories.Queries
{
    public class SearchCategoryQuery : BaseSearch, IRequest<BaseResult<CategoryViewModel>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
    }
}
