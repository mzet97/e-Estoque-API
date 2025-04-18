﻿using e_Estoque_API.Application.Categories.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Categories.Queries.Handlers;

public class SearchCategoryQueryHandler : IRequestHandler<SearchCategoryQuery, BaseResultList<CategoryViewModel>>
{
    private readonly ICategoryRepository _categoryRepository;

    public SearchCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResultList<CategoryViewModel>> Handle(
        SearchCategoryQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Category, bool>>? filter = PredicateBuilder.New<Category>(true);
        Func<IQueryable<Category>, IOrderedQueryable<Category>>? ordeBy = null;
        string includes = "Products,Taxs";

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
            filter = filter.And(x => x.Description == request.ShortDescription);
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

        var result = await _categoryRepository
            .SearchAsync(
                filter,
                ordeBy,
                includes,
                request.PageSize,
                request.PageIndex);

        return new BaseResultList<CategoryViewModel>(
            result.Data.Select(x => CategoryViewModel.FromEntity(x)).ToList(), result.PagedResult);
    }
}