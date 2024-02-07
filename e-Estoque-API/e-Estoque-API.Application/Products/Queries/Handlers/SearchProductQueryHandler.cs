using e_Estoque_API.Application.Products.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Products.Queries.Handlers;

public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, BaseResult<ProductViewModel>>
{
    private readonly IProductRepository _productRepository;

    public SearchProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<BaseResult<ProductViewModel>> Handle(
        SearchProductQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Product, bool>>? filter = PredicateBuilder.New<Product>(true);
        Func<IQueryable<Product>, IOrderedQueryable<Product>>? ordeBy = null;

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            filter = filter.And(x => x.Name == request.Name);
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            filter = filter.And(x => x.Description == request.Description);
        }

        if (!string.IsNullOrWhiteSpace(request.ShortDescription))
        {
            filter = filter.And(x => x.ShortDescription == request.ShortDescription);
        }

        if (request.Price != default)
        {
            filter = filter.And(x => x.Price == request.Price);
        }

        if (request.Weight != default)
        {
            filter = filter.And(x => x.Weight == request.Weight);
        }

        if (request.Height != default)
        {
            filter = filter.And(x => x.Height == request.Height);
        }

        if (request.Length != default)
        {
            filter = filter.And(x => x.Length == request.Length);
        }

        if (request.IdCategory != Guid.Empty)
        {
            filter = filter.And(x => x.IdCategory == request.IdCategory);
        }

        if (request.IdCompany != Guid.Empty)
        {
            filter = filter.And(x => x.IdCompany == request.IdCompany);
        }

        if (request.Id != Guid.Empty)
        {
            filter = filter.And(x => x.Id == request.Id);
        }

        if (request.CreatedAt != default)
        {
            filter = filter.And(x => x.CreatedAt == request.CreatedAt);
        }

        if (request.UpdatedAt != default)
        {
            filter = filter.And(x => x.UpdatedAt == request.UpdatedAt);
        }

        if (request.DeletedAt.HasValue || request.DeletedAt != default)
        {
            filter = filter.And(x => x.DeletedAt == request.DeletedAt);
        }

        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            switch (request.Order)
            {
                case "Id":
                    ordeBy = x => x.OrderBy(n => n.Id);
                    break;

                case "Name":
                    ordeBy = x => x.OrderBy(n => n.Name);
                    break;

                case "Description":
                    ordeBy = x => x.OrderBy(n => n.Description);
                    break;

                case "ShortDescription":
                    ordeBy = x => x.OrderBy(n => n.ShortDescription);
                    break;

                case "Price":
                    ordeBy = x => x.OrderBy(n => n.Price);
                    break;

                case "Weight":
                    ordeBy = x => x.OrderBy(n => n.Weight);
                    break;

                case "Height":
                    ordeBy = x => x.OrderBy(n => n.Height);
                    break;

                case "Length":
                    ordeBy = x => x.OrderBy(n => n.Length);
                    break;

                case "CreatedAt":
                    ordeBy = x => x.OrderBy(n => n.CreatedAt);
                    break;

                case "UpdatedAt":
                    ordeBy = x => x.OrderBy(n => n.UpdatedAt);
                    break;

                case "DeletedAt":
                    ordeBy = x => x.OrderBy(n => n.DeletedAt);
                    break;

                default:
                    ordeBy = x => x.OrderBy(n => n.Id);
                    break;
            }
        }

        var result = await _productRepository
            .Search(
                filter,
                ordeBy,
                request.PageSize,
                request.PageIndex);

        return new BaseResult<ProductViewModel>(
            result.Data.Select(x => ProductViewModel.FromEntity(x)).ToList(), result.PagedResult);
    }
}