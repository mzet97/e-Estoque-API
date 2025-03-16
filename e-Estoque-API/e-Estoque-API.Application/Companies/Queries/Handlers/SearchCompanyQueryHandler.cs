using e_Estoque_API.Application.Companies.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Companies.Queries.Handlers;

public class SearchCompanyQueryHandler : IRequestHandler<SearchCompanyQuery, BaseResultList<CompanyViewModel>>
{
    private readonly ICompanyRepository _companyRepository;

    public SearchCompanyQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<BaseResultList<CompanyViewModel>> Handle(
        SearchCompanyQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Company, bool>>? filter = PredicateBuilder.New<Company>(true);
        Func<IQueryable<Company>, IOrderedQueryable<Company>>? ordeBy = null;

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

        var result = await _companyRepository
            .SearchAsync(
                filter,
                ordeBy,
                "",
                request.PageSize,
                request.PageIndex);

        return new BaseResultList<CompanyViewModel>(
            result.Data.Select(x => CompanyViewModel.FromEntity(x)).ToList(), result.PagedResult);
    }
}