using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
    public class TaxRepository : Repository<Tax>, ITaxRepository
    {
        public TaxRepository(EstoqueDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Tax>> Find(Expression<Func<Tax, bool>> predicate)
        {
            return await DbSet
                .AsNoTracking()
                .Include("Category")
                .Where(predicate)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Tax>> GetAll()
        {
            return await DbSet
                .Include("Category")
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Tax?> GetById(Guid id)
        {
            return await DbSet
                .Include("Category")
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

        public override async Task<BaseResult<Tax>> Search(
            Expression<Func<Tax, bool>>? predicate = null,
            Func<IQueryable<Tax>, IOrderedQueryable<Tax>>? orderBy = null,
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
                             .Where(predicate);
            }

            query = query.Include("Category")
                         .OrderBy(x => x.Id)
                         .Skip(skip)
                         .Take(pageSize);

            if (orderBy != null)
            {
                var data = await orderBy(query).ToListAsync();
                return new BaseResult<Tax>(data, paged);
            }

            return new BaseResult<Tax>(await query.ToListAsync(), paged);
        }
    }
}
