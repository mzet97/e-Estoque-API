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
            .Include("Product.Category")
            .Include("Product.Company")
            .Include("Product.Company.CompanyAddress")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Inventory>> GetAll()
    {
        return await DbSet
            .Include("Product")
            .Include("Product.Category")
            .Include("Product.Company")
            .Include("Product.Company.CompanyAddress")
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<Inventory?> GetById(Guid id)
    {
        return await DbSet
            .Include("Product")
            .Include("Product.Category")
            .Include("Product.Company")
            .Include("Product.Company.CompanyAddress")
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task<BaseResult<Inventory>> Search(
        Expression<Func<Inventory, bool>>? predicate = null,
        Func<IQueryable<Inventory>, IOrderedQueryable<Inventory>>? orderBy = null,
        int pageSize = 10, int page = 1)
    {
        var query = DbSet.AsQueryable();

        var paged = PagedResult.Create(page, pageSize, query.Count());

        if (predicate != null)
        {
            query = query.Include("Product")
                .Include("Product.Category")
                .Include("Product.Company")
                .Include("Product.Company.CompanyAddress")
                .Where(predicate);
        }

        query = query.Include("Product")
            .Include("Product.Category")
            .Include("Product.Company")
            .Include("Product.Company.CompanyAddress")
            .OrderBy(x => x.Id)
            .Skip(paged.Skip())
            .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResult<Inventory>(data, paged);
        }

        return new BaseResult<Inventory>(await query.ToListAsync(), paged);
    }
}