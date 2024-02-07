﻿using e_Estoque_API.Application.Taxes.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Taxes.Queries.Handlers
{
    public class SearchTaxQueryHandler : IRequestHandler<SearchTaxQuery, BaseResult<TaxViewModel>>
    {
        private readonly ITaxRepository _taxRepository;

        public SearchTaxQueryHandler(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public async Task<BaseResult<TaxViewModel>> Handle(SearchTaxQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Tax, bool>>? filter = null;
            Func<IQueryable<Tax>, IOrderedQueryable<Tax>>? ordeBy = null;

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                filter = filter.And(x => x.Name == request.Name);
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Tax>(true);
                }

                filter = filter.And(x => x.Description == request.Description);
            }

            if (request.Percentage != 0)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Tax>(true);
                }

                filter = filter.And(x => x.Percentage == request.Percentage);
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
                    case "Percentage":
                        ordeBy = x => x.OrderBy(n => n.Percentage);
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

            if (request.IdCategory != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Tax>(true);
                }

                filter = filter.And(x => x.IdCategory == request.IdCategory);
            }

            if (request.Id != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Tax>(true);
                }

                filter = filter.And(x => x.Id == request.Id);
            }


            if (request.CreatedAt != default)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Tax>(true);
                }

                filter = filter.And(x => x.CreatedAt == request.CreatedAt);
            }

            if (request.UpdatedAt != default)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Tax>(true);
                }

                filter = filter.And(x => x.UpdatedAt == request.UpdatedAt);
            }

            if (request.DeletedAt.HasValue || request.DeletedAt != default)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Tax>(true);
                }

                filter = filter.And(x => x.DeletedAt == request.DeletedAt);
            }

            var result = await _taxRepository
                .Search(
                    filter,
                    ordeBy,
                    request.PageSize,
                    request.PageIndex);

            return new BaseResult<TaxViewModel>(
                result.Data.Select(x => TaxViewModel.FromEntity(x)).ToList(), result.PagedResult);
        }
    }
}
