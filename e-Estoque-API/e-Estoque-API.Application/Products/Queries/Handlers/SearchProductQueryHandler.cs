using e_Estoque_API.Application.Products.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Products.Queries.Handlers;

public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, BaseResultList<ProductViewModel>>
{
    private readonly IProductRepository _productRepository;

    public SearchProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<BaseResultList<ProductViewModel>> Handle(
     SearchProductQuery request,
     CancellationToken cancellationToken)
    {
        Expression<Func<Product, bool>>? filter = PredicateBuilder.New<Product>(true);
        Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;

        filter = AddFilterIfNotNullOrWhiteSpace(request.Name, filter, x => x.Name == request.Name);
        filter = AddFilterIfNotNullOrWhiteSpace(request.Description, filter, x => x.Description == request.Description);
        filter = AddFilterIfNotNullOrWhiteSpace(request.ShortDescription, filter, x => x.ShortDescription == request.ShortDescription);

        filter = AddFilterIfNotDefault(request.Price, filter, x => x.Price == request.Price);
        filter = AddFilterIfNotDefault(request.Weight, filter, x => x.Weight == request.Weight);
        filter = AddFilterIfNotDefault(request.Height, filter, x => x.Height == request.Height);
        filter = AddFilterIfNotDefault(request.Length, filter, x => x.Length == request.Length);
        filter = AddFilterIfNotDefault(request.CreatedAt, filter, x => x.CreatedAt == request.CreatedAt);
        filter = AddFilterIfNotDefault(request.UpdatedAt, filter, x => x.UpdatedAt == request.UpdatedAt);

        filter = AddFilterIfNotEmptyGuid(request.IdCategory, filter, x => x.IdCategory == request.IdCategory);
        filter = AddFilterIfNotEmptyGuid(request.IdCompany, filter, x => x.IdCompany == request.IdCompany);
        filter = AddFilterIfNotEmptyGuid(request.Id, filter, x => x.Id == request.Id);

        if (request.DeletedAt.HasValue || request.DeletedAt != default)
        {
            filter = filter.And(x => x.DeletedAt == request.DeletedAt);
        }

        orderBy = GetOrderByFunc(request.Order);

        var result = await _productRepository
            .SearchAsync(
                filter,
                orderBy,
                request.PageSize,
                request.PageIndex);

        return new BaseResultList<ProductViewModel>(
            result.Data.Select(x => ProductViewModel.FromEntity(x)).ToList(), result.PagedResult);
    }

    private Expression<Func<Product, bool>> AddFilterIfNotNullOrWhiteSpace(string value, Expression<Func<Product, bool>> filter, Expression<Func<Product, bool>> predicate)
    {
        return !string.IsNullOrWhiteSpace(value) ? filter.And(predicate) : filter;
    }

    private Expression<Func<Product, bool>> AddFilterIfNotDefault<T>(T value, Expression<Func<Product, bool>> filter, Expression<Func<Product, bool>> predicate) where T : struct
    {
        return !EqualityComparer<T>.Default.Equals(value, default) ? filter.And(predicate) : filter;
    }

    private Expression<Func<Product, bool>> AddFilterIfNotEmptyGuid(Guid value, Expression<Func<Product, bool>> filter, Expression<Func<Product, bool>> predicate)
    {
        return value != Guid.Empty ? filter.And(predicate) : filter;
    }

    private Func<IQueryable<Product>, IOrderedQueryable<Product>> GetOrderByFunc(string? order)
    {
        return order switch
        {
            "Name" => x => x.OrderBy(n => n.Name),
            "Description" => x => x.OrderBy(n => n.Description),
            "ShortDescription" => x => x.OrderBy(n => n.ShortDescription),
            "Price" => x => x.OrderBy(n => n.Price),
            "Weight" => x => x.OrderBy(n => n.Weight),
            "Height" => x => x.OrderBy(n => n.Height),
            "Length" => x => x.OrderBy(n => n.Length),
            "CreatedAt" => x => x.OrderBy(n => n.CreatedAt),
            "UpdatedAt" => x => x.OrderBy(n => n.UpdatedAt),
            "DeletedAt" => x => x.OrderBy(n => n.DeletedAt),
            _ => x => x.OrderBy(n => n.Id),
        };
    }

}