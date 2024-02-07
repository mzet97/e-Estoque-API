using e_Estoque_API.Application.Customers.ViewModels;
using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using LinqKit;
using MediatR;
using System.Linq.Expressions;

namespace e_Estoque_API.Application.Customers.Queries.Handlers
{
    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, BaseResult<CustomerViewModel>>
    {
        private readonly ICustomerRepository _customerRepository;

        public SearchCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<BaseResult<CustomerViewModel>> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Customer, bool>>? filter = null;
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? ordeBy = null;

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                filter = filter.And(x => x.Name == request.Name);
            }

            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Customer>(true);
                }

                filter = filter.And(x => x.Description == request.Description);
            }

            if (!string.IsNullOrWhiteSpace(request.DocId))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Customer>(true);
                }

                filter = filter.And(x => x.DocId == request.DocId);
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Customer>(true);
                }

                filter = filter.And(x => x.PhoneNumber == request.PhoneNumber);
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Customer>(true);
                }

                filter = filter.And(x => x.Email == request.Email);
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
                }
            }

            if (request.Id != Guid.Empty)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Customer>(true);
                }

                filter = filter.And(x => x.Id == request.Id);
            }


            if (request.CreatedAt != default)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Customer>(true);
                }

                filter = filter.And(x => x.CreatedAt == request.CreatedAt);
            }

            if (request.UpdatedAt != default)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Customer>(true);
                }

                filter = filter.And(x => x.UpdatedAt == request.UpdatedAt);
            }

            if (request.DeletedAt.HasValue || request.DeletedAt != default)
            {
                if (filter == null)
                {
                    filter = PredicateBuilder.New<Customer>(true);
                }

                filter = filter.And(x => x.DeletedAt == request.DeletedAt);
            }

            var result = await _customerRepository
                .Search(
                    filter,
                    ordeBy,
                    request.PageSize,
                    request.PageIndex);

            return new BaseResult<CustomerViewModel>(
                result.Data.Select(x => CustomerViewModel.FromEntity(x)).ToList(), result.PagedResult);
        }
    }
}
