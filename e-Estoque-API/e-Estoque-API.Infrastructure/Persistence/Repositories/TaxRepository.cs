using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
    public class TaxRepository : Repository<Tax>, ITaxRepository
    {
        public TaxRepository(EstoqueDbContext db) : base(db)
        {
        }
    }
}
