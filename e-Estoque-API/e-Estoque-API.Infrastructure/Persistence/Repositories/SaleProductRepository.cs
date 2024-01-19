using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
    public class SaleProductRepository : Repository<SaleProduct>, ISaleProductRepository
    {
        public SaleProductRepository(EstoqueDbContext db) : base(db)
        {
        }
    }
}
