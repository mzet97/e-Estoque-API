﻿using e_Estoque_API.Core.Entities;
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

    public override async Task<IEnumerable<Sale>> FindAsync(Expression<Func<Sale, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Include("Customer")
            .Include("SaleProduct")
            .Include("SaleProduct.Product")
            .Where(predicate)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await DbSet
             .Include("Customer")
             .Include("SaleProduct")
             .Include("SaleProduct.Product")
             .AsNoTracking()
             .ToListAsync();
    }

    public override async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await DbSet
            .Include("Customer")
            .Include("SaleProduct")
            .Include("SaleProduct.Product")
            .AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public override async Task<BaseResultList<Sale>> SearchAsync(
        Expression<Func<Sale, bool>>? predicate = null,
        Func<IQueryable<Sale>, IOrderedQueryable<Sale>>? orderBy = null,
        int pageSize = 10, int page = 1)
    {
        var query = DbSet.AsQueryable();

        var paged = PagedResult.Create(page, pageSize, query.Count());

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
                     .Skip(paged.Skip())
                     .Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResultList<Sale>(data, paged);
        }

        return new BaseResultList<Sale>(await query.ToListAsync(), paged);
    }
}