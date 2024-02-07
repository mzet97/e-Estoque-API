using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public class SaleRepository : Repository<Sale>, ISaleRepository
{
    public SaleRepository(EstoqueDbContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Sale>> Find(Expression<Func<Sale, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Customer")
            .Include("SaleProduct")
            .Include("SaleProduct.Product")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Sale>> GetAll()
    {
        return await DbSet
             .Include("Customer")
             .Include("SaleProduct")
             .Include("SaleProduct.Product")
             .AsNoTracking()
             .ToListAsync();
    }

    public override async Task<Sale?> GetById(Guid id)
    {
        return await DbSet
            .Include("Customer")
            .Include("SaleProduct")
            .Include("SaleProduct.Product")
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

    public override async Task<BaseResult<Sale>> Search(
        Expression<Func<Sale, bool>>? predicate = null,
        Func<IQueryable<Sale>, IOrderedQueryable<Sale>>? orderBy = null,
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
            query = query.Include("Customer")
                         .Include("SaleProduct")
                         .Include("SaleProduct.Product")
                         .Where(predicate);
        }

        query = query.Include("Customer")
                     .Include("SaleProduct")
                     .Include("SaleProduct.Product")
                     .OrderBy(x => x.Id)
                     .Skip(skip)
                     .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResult<Sale>(data, paged);
        }

        return new BaseResult<Sale>(await query.ToListAsync(), paged);
    }
}