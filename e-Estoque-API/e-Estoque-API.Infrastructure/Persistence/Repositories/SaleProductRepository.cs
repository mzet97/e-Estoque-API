using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
    public class SaleProductRepository : Repository<SaleProduct>, ISaleProductRepository
    {
        public SaleProductRepository(EstoqueDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<SaleProduct>> Find(Expression<Func<SaleProduct, bool>> predicate)
        {
            return await DbSet
                .AsNoTracking()
                .Include("Product")
                .Include("Sale")
                .Where(predicate)
                .ToListAsync();
        }

        public override async Task<IEnumerable<SaleProduct>> GetAll()
        {
            return await DbSet
                 .Include("Product")
                 .Include("Sale")
                 .AsNoTracking()
                 .ToListAsync();
        }

        public override async Task<SaleProduct?> GetById(Guid id)
        {
            return await DbSet
                .Include("Product")
                .Include("Sale")
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

        public override async Task<BaseResult<SaleProduct>> Search(
            Expression<Func<SaleProduct, bool>>? predicate = null,
            Func<IQueryable<SaleProduct>, IOrderedQueryable<SaleProduct>>? orderBy = null,
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
                             .Include("Sale")
                             .Where(predicate);
            }

            query = query.Include("Product")
                         .Include("Sale")
                         .OrderBy(x => x.Id)
                         .Skip(skip)
                         .Take(pageSize);

            if (orderBy != null)
            {
                var data = await orderBy(query).ToListAsync();
                return new BaseResult<SaleProduct>(data, paged);
            }

            return new BaseResult<SaleProduct>(await query.ToListAsync(), paged);
        }
    }
}
