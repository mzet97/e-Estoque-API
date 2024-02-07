using e_Estoque_API.Application.Inventories.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Inventories.Queries.Handlers;

public class SearchInventoryQueryHandler : IRequestHandler<SearchInventoryQuery, BaseResult<InventoryViewModel>>
{
    private readonly IInventoryRepository _inventoryRepository;

    public SearchInventoryQueryHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<BaseResult<InventoryViewModel>> Handle(SearchInventoryQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Inventory, bool>>? filter = null;
        Func<IQueryable<Inventory>, IOrderedQueryable<Inventory>>? ordeBy = null;

        if (request.Quantity != default)
        {
            filter = filter.And(x => x.Quantity == request.Quantity);
        }

        if (request.DateOrder != default)
        {
            if (filter == null)
            {
                filter = PredicateBuilder.New<Inventory>(true);
            }

            filter = filter.And(x => x.DateOrder == request.DateOrder);
        }

        if (!string.IsNullOrWhiteSpace(request.Order))
        {
            switch (request.Order)
            {
                case "Id":
                    ordeBy = x => x.OrderBy(n => n.Id);
                    break;

                case "Quantity":
                    ordeBy = x => x.OrderBy(n => n.Quantity);
                    break;

                case "DateOrder":
                    ordeBy = x => x.OrderBy(n => n.DateOrder);
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
            }
        }

        if (request.IdProduct != Guid.Empty)
        {
            if (filter == null)
            {
                filter = PredicateBuilder.New<Inventory>(true);
            }

            filter = filter.And(x => x.IdProduct == request.IdProduct);
        }

        if (request.Id != Guid.Empty)
        {
            if (filter == null)
            {
                filter = PredicateBuilder.New<Inventory>(true);
            }

            filter = filter.And(x => x.Id == request.Id);
        }

        if (request.CreatedAt != default)
        {
            if (filter == null)
            {
                filter = PredicateBuilder.New<Inventory>(true);
            }

            filter = filter.And(x => x.CreatedAt == request.CreatedAt);
        }

        if (request.UpdatedAt != default)
        {
            if (filter == null)
            {
                filter = PredicateBuilder.New<Inventory>(true);
            }

            filter = filter.And(x => x.UpdatedAt == request.UpdatedAt);
        }

        if (request.DeletedAt.HasValue || request.DeletedAt != default)
        {
            if (filter == null)
            {
                filter = PredicateBuilder.New<Inventory>(true);
            }

            filter = filter.And(x => x.DeletedAt == request.DeletedAt);
        }

        var result = await _inventoryRepository
            .Search(
                filter,
                ordeBy,
                request.PageSize,
                request.PageIndex);

        return new BaseResult<InventoryViewModel>(
            result.Data.Select(x => InventoryViewModel.FromEntity(x)).ToList(), result.PagedResult);
    }
}