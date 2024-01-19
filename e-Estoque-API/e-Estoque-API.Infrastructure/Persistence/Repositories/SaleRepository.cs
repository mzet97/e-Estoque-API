using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(EstoqueDbContext db) : base(db)
        {
        }
    }
}
