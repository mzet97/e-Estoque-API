using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(EstoqueDbContext db) : base(db)
        {
        }
    }
}
