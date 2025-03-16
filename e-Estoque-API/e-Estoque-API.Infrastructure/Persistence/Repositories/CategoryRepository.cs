using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(EstoqueDbContext db) : base(db)
    {
    }

    public override async Task<Category?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Products")
            .Include("Taxs")
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await DbSet
            .AsNoTracking()
            .Include("Products")
            .Include("Taxs")
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Products")
            .Include("Taxs")
            .Where(predicate)
            .ToListAsync();
    }

}