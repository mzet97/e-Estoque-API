﻿using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public class TaxRepository : Repository<Tax>, ITaxRepository
{
    public TaxRepository(EstoqueDbContext db) : base(db)
    {
    }

    public override async Task<IEnumerable<Tax>> FindAsync(Expression<Func<Tax, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Category")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Tax>> GetAllAsync()
    {
        return await DbSet
            .Include("Category")
            .AsNoTracking()
            .ToListAsync();
    }

    public override async Task<Tax?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Include("Category")
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task<BaseResultList<Tax>> SearchAsync(
        Expression<Func<Tax, bool>>? predicate = null,
        Func<IQueryable<Tax>, IOrderedQueryable<Tax>>? orderBy = null,
        int pageSize = 10, int page = 1)
    {
        var query = DbSet.AsQueryable();

        var paged = PagedResult.Create(page, pageSize, query.Count());

        if (predicate != null)
        {
            query = query.Include("Category")
                         .Where(predicate);
        }

        query = query.Include("Category")
                     .OrderBy(x => x.Id)
                     .Skip(paged.Skip())
                     .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResultList<Tax>(data, paged);
        }

        return new BaseResultList<Tax>(await query.ToListAsync(), paged);
    }
}