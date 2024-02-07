using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(EstoqueDbContext db) : base(db)
    {
    }

    public override async Task<Category?> GetById(Guid id)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Products")
            .Include("Taxs")
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task Remove(Guid id)
    {
        var entity = await GetById(id);
        entity.UpdatedAt = DateTime.UtcNow;
        entity.DeletedAt = DateTime.UtcNow;
        DbSet.Update(entity);
        await Db.SaveChangesAsync();
    }

    public override async Task<IEnumerable<Category>> GetAll()
    {
        return await DbSet
            .AsNoTracking()
            .Include("Products")
            .Include("Taxs")
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Products")
            .Include("Taxs")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<BaseResult<Category>> Search(
        Expression<Func<Category, bool>>? predicate = null,
        Func<IQueryable<Category>, IOrderedQueryable<Category>>? orderBy = null,
        int pageSize = 10, int page = 1)
    {
        var query = DbSet.AsQueryable();

        var paged = PagedResult.Create(page, pageSize, query.Count());

        if (predicate != null)
        {
            query = query.Include("Products")
                         .Include("Taxs")
                         .Where(predicate);
        }

        query = query.Include("Products")
                     .Include("Taxs")
                     .OrderBy(x => x.Id)
                     .Skip(paged.Skip())
                     .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResult<Category>(data, paged);
        }

        return new BaseResult<Category>(await query.ToListAsync(), paged);
    }
}