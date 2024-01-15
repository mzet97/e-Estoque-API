using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.Context;

namespace e_Estoque_API.Infrastructure.Repositories
{
    public class TaxRepository : Repository<Tax>, ITaxRepository
    {
        public TaxRepository(DataIdentityDbContext db) : base(db)
        {
        }
    }
}
