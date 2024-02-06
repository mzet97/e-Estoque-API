using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
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
                .Where(predicate)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await DbSet
                .Include("Category")
                .Include("Company")
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Product?> GetById(Guid id)
        {
            return await DbSet
                .Include("Category")
                .Include("Company")
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public override async Task Remove(Guid id)
        {
            var entity = await GetById(id);
            entity.UpdatedAt = DateTime.Now;
            entity.DeletedAt = DateTime.Now;
            DbSet.Update(entity);
        }

        public override async Task<BaseResult<Product>> Search(
            Expression<Func<Product, bool>>? predicate = null,
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null,
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
                query = query.Include("Category")
                             .Include("Company")
                             .Where(predicate);
            }

            query = query.Include("Category")
                         .Include("Company")
                         .OrderBy(x => x.Id)
                         .Skip(skip)
                         .Take(pageSize);

            if (orderBy != null)
            {
                var data = await orderBy(query).ToListAsync();
                return new BaseResult<Product>(data, paged);
            }

            return new BaseResult<Product>(await query.ToListAsync(), paged);
        }
    }
}
