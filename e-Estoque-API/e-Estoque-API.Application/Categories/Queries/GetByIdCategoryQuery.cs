using e_Estoque_API.Application.Categories.ViewModels;
using MediatR;

namespace e_Estoque_API.Application.Categories.Queries
{
    public class GetByIdCategoryQuery : IRequest<CategoryViewModel>
    {
        public Guid Id { get; set; }

        public GetByIdCategoryQuery(Guid id)
        {
            Id = id;
        }
    }
}
