using e_Estoque_API.Application.Sales.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Enums;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Sales.Queries.Handlers;

public class SearchSaleQueryHandler : IRequestHandler<SearchSaleQuery, BaseResultList<SaleViewModel>>
{
    private readonly ISaleRepository _saleRepository;

    public SearchSaleQueryHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<BaseResultList<SaleViewModel>> Handle(
     SearchSaleQuery request,
     CancellationToken cancellationToken)
    {
        Expression<Func<Sale, bool>>? filter = PredicateBuilder.New<Sale>(true);
        Func<IQueryable<Sale>, IOrderedQueryable<Sale>>? orderBy = null;

        filter = AddFilterIfNotNullOrWhiteSpace(request.SaleType, filter, x => SaleTypeHelper.FromStringForId(request.SaleType) == (int)x.SaleType);
        filter = AddFilterIfNotNullOrWhiteSpace(request.PaymentType, filter, x => PaymentTypeHelper.FromStringForId(request.PaymentType) == (int)x.PaymentType);

        filter = AddFilterIfNotDefault(request.Quantity, filter, x => x.Quantity == request.Quantity);
        filter = AddFilterIfNotDefault(request.TotalPrice, filter, x => x.TotalPrice == request.TotalPrice);
        filter = AddFilterIfNotDefault(request.TotalTax, filter, x => x.TotalTax == request.TotalTax);
        filter = AddFilterIfNotDefault(request.DeliveryDate, filter, x => x.DeliveryDate == request.DeliveryDate);
        filter = AddFilterIfNotDefault(request.SaleDate, filter, x => x.SaleDate == request.SaleDate);
        filter = AddFilterIfNotDefault(request.PaymentDate, filter, x => x.PaymentDate == request.PaymentDate);
        filter = AddFilterIfNotDefault(request.CreatedAt, filter, x => x.CreatedAt == request.CreatedAt);
        filter = AddFilterIfNotDefault(request.UpdatedAt, filter, x => x.UpdatedAt == request.UpdatedAt);

        filter = AddFilterIfNotEmptyGuid(request.IdCustomer, filter, x => x.IdCustomer == request.IdCustomer);
        filter = AddFilterIfNotEmptyGuid(request.IdProduct, filter, x => x.SaleProducts.Any(y => y.IdProduct == request.IdProduct));
        filter = AddFilterIfNotEmptyGuid(request.Id, filter, x => x.Id == request.Id);

        if (request.DeletedAt.HasValue || request.DeletedAt != default)
        {
            filter = filter.And(x => x.DeletedAt == request.DeletedAt);
        }

        orderBy = GetOrderByFunc(request.Order);

        var result = await _saleRepository
            .SearchAsync(
                filter,
                orderBy,
                request.PageSize,
                request.PageIndex);

        return new BaseResultList<SaleViewModel>(
            result.Data.Select(x => SaleViewModel.FromEntity(x))
            .ToList(), 
            result.PagedResult);
    }

    private Expression<Func<Sale, bool>> AddFilterIfNotNullOrWhiteSpace(
        string value,
        Expression<Func<Sale, bool>> filter,
        Expression<Func<Sale, bool>> predicate)
    {
        return !string.IsNullOrWhiteSpace(value) ? filter.And(predicate) : filter;
    }

    private Expression<Func<Sale, bool>> AddFilterIfNotDefault<T>(
        T value,
        Expression<Func<Sale, bool>> filter,
        Expression<Func<Sale, bool>> predicate) where T : struct
    {
        return !EqualityComparer<T>.Default.Equals(value, default) ? filter.And(predicate) : filter;
    }

    private Expression<Func<Sale, bool>> AddFilterIfNotEmptyGuid(
        Guid value,
        Expression<Func<Sale, bool>> filter,
        Expression<Func<Sale, bool>> predicate)
    {
        return value != Guid.Empty ? filter.And(predicate) : filter;
    }

    private Func<IQueryable<Sale>, IOrderedQueryable<Sale>> GetOrderByFunc(string? order)
    {
        return order switch
        {
            "Quantity" => x => x.OrderBy(n => n.Quantity),
            "TotalPrice" => x => x.OrderBy(n => n.TotalPrice),
            "TotalTax" => x => x.OrderBy(n => n.TotalTax),
            "SaleType" => x => x.OrderBy(n => n.SaleType),
            "PaymentType" => x => x.OrderBy(n => n.PaymentType),
            "DeliveryDate" => x => x.OrderBy(n => n.DeliveryDate),
            "SaleDate" => x => x.OrderBy(n => n.SaleDate),
            "PaymentDate" => x => x.OrderBy(n => n.PaymentDate),
            "IdCustomer" => x => x.OrderBy(n => n.IdCustomer),
            "CreatedAt" => x => x.OrderBy(n => n.CreatedAt),
            "UpdatedAt" => x => x.OrderBy(n => n.UpdatedAt),
            "DeletedAt" => x => x.OrderBy(n => n.DeletedAt),
            _ => x => x.OrderBy(n => n.Id),
        };
    }

}