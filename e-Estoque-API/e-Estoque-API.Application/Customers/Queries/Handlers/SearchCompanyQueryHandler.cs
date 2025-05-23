﻿using e_Estoque_API.Application.Customers.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Customers.Queries.Handlers;

public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, BaseResultList<CustomerViewModel>>
{
    private readonly ICustomerRepository _customerRepository;

    public SearchCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<BaseResultList<CustomerViewModel>> Handle(
        SearchCustomerQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Customer, bool>>? filter = PredicateBuilder.New<Customer>(true);
        Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? ordeBy = null;

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            filter = filter.And(x => x.Name == request.Name);
        }

        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            filter = filter.And(x => x.Description == request.Description);
        }

        if (!string.IsNullOrWhiteSpace(request.DocId))
        {
            filter = filter.And(x => x.DocId == request.DocId);
        }

        if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            filter = filter.And(x => x.PhoneNumber == request.PhoneNumber);
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            filter = filter.And(x => x.Email == request.Email);
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

                case "DocId":
                    ordeBy = x => x.OrderBy(n => n.DocId);
                    break;

                case "Email":
                    ordeBy = x => x.OrderBy(n => n.Email);
                    break;

                case "PhoneNumber":
                    ordeBy = x => x.OrderBy(n => n.PhoneNumber);
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

        var result = await _customerRepository
            .SearchAsync(
                filter,
                ordeBy,
                "",
                request.PageSize,
                request.PageIndex);

        return new BaseResultList<CustomerViewModel>(
            result.Data.Select(x => CustomerViewModel.FromEntity(x)).ToList(), result.PagedResult);
    }
}