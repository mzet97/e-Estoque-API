using e_Estoque_API.Application.Companies.ViewModels;
using MediatR;

namespace e_Estoque_API.Application.Companies.Queries
{
    public class GetByIdCompanyQuery : IRequest<CompanyViewModel>
    {
        public Guid Id { get; set; }

        public GetByIdCompanyQuery(Guid id)
        {
            Id = id;
        }
    }
}
