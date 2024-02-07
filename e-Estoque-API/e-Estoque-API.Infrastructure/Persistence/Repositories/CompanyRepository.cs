using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(EstoqueDbContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Company>> Find(Expression<Func<Company, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("CompanyAddress")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Company>> GetAll()
    {
        return await DbSet
          .AsNoTracking()
          .Include("CompanyAddress")
          .ToListAsync();
    }

    public override async Task<Company?> GetById(Guid id)
    {
        return await DbSet
           .AsNoTracking()
           .Include("CompanyAddress")
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

    public override async Task<BaseResult<Company>> Search(
        Expression<Func<Company, bool>>? predicate = null,
        Func<IQueryable<Company>, IOrderedQueryable<Company>>? orderBy = null,
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
            query = query.Include("CompanyAddress")
                         .Where(predicate);
        }

        query = query.Include("CompanyAddress")
                     .OrderBy(x => x.Id)
                     .Skip(skip)
                     .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResult<Company>(data, paged);
        }

        return new BaseResult<Company>(await query.ToListAsync(), paged);
    }
}