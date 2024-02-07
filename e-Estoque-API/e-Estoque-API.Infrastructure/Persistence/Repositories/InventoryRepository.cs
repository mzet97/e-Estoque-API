using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public class InventoryRepository : Repository<Inventory>, IInventoryRepository
{
    public InventoryRepository(EstoqueDbContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Inventory>> Find(Expression<Func<Inventory, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Product")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Inventory>> GetAll()
    {
        return await DbSet
            .Include("Product")
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<Inventory?> GetById(Guid id)
    {
        return await DbSet
            .Include("Product")
            .AsNoTracking()
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

    public override async Task<BaseResult<Inventory>> Search(
        Expression<Func<Inventory, bool>>? predicate = null,
        Func<IQueryable<Inventory>, IOrderedQueryable<Inventory>>? orderBy = null,
        int pageSize = 10, int page = 1)
    {
        var query = DbSet.AsQueryable();

        var paged = new PagedResult();
        paged.CurrentPage = page;
        paged.PageSize = pageSize;
        paged.RowCount = query.Count();
        var pageCount = (double)paged.RowCount / pageSize;
        paged.PageCount = (int)Math.Ceiling(pageCount);
        var skip = (page - 1) * pageSize;

        if (predicate != null)
        {
            query = query.Include("Product")
                         .Where(predicate);
        }

        query = query.Include("Product")
                     .OrderBy(x => x.Id)
                     .Skip(skip)
                     .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResult<Inventory>(data, paged);
        }

        return new BaseResult<Inventory>(await query.ToListAsync(), paged);
    }
}