using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public class SaleProductRepository : Repository<SaleProduct>, ISaleProductRepository
{
    public SaleProductRepository(EstoqueDbContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<SaleProduct>> FindAsync(Expression<Func<SaleProduct, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Product")
            .Include("Sale")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<IEnumerable<SaleProduct>> GetAllAsync()
    {
        return await DbSet
             .Include("Product")
             .Include("Sale")
             .AsNoTracking()
             .ToListAsync();
    }

    public override async Task<SaleProduct?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Include("Product")
            .Include("Sale")
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task<BaseResultList<SaleProduct>> SearchAsync(
        Expression<Func<SaleProduct, bool>>? predicate = null,
        Func<IQueryable<SaleProduct>, IOrderedQueryable<SaleProduct>>? orderBy = null,
        int pageSize = 10, int page = 1)
    {
        var query = DbSet.AsQueryable();

        var paged = PagedResult.Create(page, pageSize, query.Count());

        if (predicate != null)
        {
            query = query.Include("Product")
                         .Include("Sale")
                         .Where(predicate);
        }

        query = query.Include("Product")
                     .Include("Sale")
                     .OrderBy(x => x.Id)
                     .Skip(paged.Skip())
                     .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResultList<SaleProduct>(data, paged);
        }

        return new BaseResultList<SaleProduct>(await query.ToListAsync(), paged);
    }
}