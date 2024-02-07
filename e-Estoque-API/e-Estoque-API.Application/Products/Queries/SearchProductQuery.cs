using e_Estoque_API.Application.Dtos.InputModels;
using e_Estoque_API.Application.Products.ViewModels;
using e_Estoque_API.Core.Models;
using MediatR;

namespace e_Estoque_API.Application.Products.Queries
{
    public class SearchProductQuery : BaseSearch, IRequest<BaseResult<ProductViewModel>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }

        public Guid IdCategory { get; set; }
        public Guid IdCompany { get; set; }
    }
}
