using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(EstoqueDbContext db) : base(db)
        {
        }

        public override async Task<IEnumerable<Customer>> Find(Expression<Func<Customer, bool>> predicate)
        {
            return await DbSet
                .AsNoTracking()
                .Include("CompanyAddress")
                .Where(predicate)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Customer>> GetAll()
        {
            return await DbSet
              .AsNoTracking()
              .Include("CustomerAddress")
              .ToListAsync();
        }

        public override async Task<Customer?> GetById(Guid id)
        {
            return await DbSet
               .AsNoTracking()
               .Include("CustomerAddress")
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

        public override async Task<BaseResult<Customer>> Search(
            Expression<Func<Customer, bool>>? predicate = null,
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy = null,
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
                query = query.Include("CustomerAddress")
                             .Where(predicate);
            }

            query = query.Include("CustomerAddress")
                         .OrderBy(x => x.Id)
                         .Skip(skip)
                         .Take(pageSize);

            if (orderBy != null)
            {
                var data = await orderBy(query).ToListAsync();
                return new BaseResult<Customer>(data, paged);
            }

            return new BaseResult<Customer>(await query.ToListAsync(), paged);
        }
    }
}
