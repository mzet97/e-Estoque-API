using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(EstoqueDbContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Category")
            .Include("Company")
            .Include("Company.CompanyAddress")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Product>> GetAll()
    {
        return await DbSet
            .Include("Category")
            .Include("Company")
            .Include("Company.CompanyAddress")
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<Product?> GetById(Guid id)
    {
        return await DbSet
            .Include("Category")
            .Include("Company")
            .Include("Company.CompanyAddress")
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task<BaseResult<Product>> Search(
        Expression<Func<Product, bool>>? predicate = null,
        Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
        int pageSize = 10, int page = 1)
    {
        var query = DbSet.AsQueryable();

        var paged = PagedResult.Create(page, pageSize, query.Count());

        if (predicate != null)
        {
            query = query.Include("Category")
                         .Include("Company")
                         .Include("Company.CompanyAddress")
                         .Where(predicate);
        }

        query = query.Include("Category")
                     .Include("Company")
                     .Include("Company.CompanyAddress")
                     .OrderBy(x => x.Id)
                     .Skip(paged.Skip())
                     .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResult<Product>(data, paged);
        }

        return new BaseResult<Product>(await query.ToListAsync(), paged);
    }
}