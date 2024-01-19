using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(EstoqueDbContext db) : base(db)
        {
        }
    }
}
