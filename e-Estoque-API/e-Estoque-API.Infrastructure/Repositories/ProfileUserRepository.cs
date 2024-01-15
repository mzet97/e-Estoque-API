using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;
using e_Estoque_API.Infrastructure.Context;

namespace e_Estoque_API.Infrastructure.Repositories
{
    public class ProfileUserRepository : Repository<ProfileUser>, IProfileUserRepository
    {
        public ProfileUserRepository(DataIdentityDbContext db) : base(db)
        {
        }
    }
}
