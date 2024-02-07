﻿using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using e_Estoque_API.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace e_Estoque_API.Infrastructure.Persistence.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot, new()
{
    protected readonly EstoqueDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(EstoqueDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public virtual async Task Add(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await DbSet.AddAsync(entity);

        await Db.SaveChangesAsync();
    }

    public virtual async Task<BaseResult<TEntity>> Search(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageSize = 10, int page = 1)
    {
        var query = DbSet.AsQueryable();

        var paged = PagedResult.Create(page, pageSize, query.Count());

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        query = query.OrderBy(x => x.Id).Skip(paged.Skip()).Take(pageSize);

        if (orderBy != null)
        {
            var data = await orderBy(query).ToListAsync();
            return new BaseResult<TEntity>(data, paged);
        }

        return new BaseResult<TEntity>(await query.ToListAsync(), paged);
    }

    public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task Update(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        DbSet.Update(entity);

        await Db.SaveChangesAsync();
    }

    public virtual async Task Remove(Guid id)
    {
        var entity = DbSet.Find(id);

        if (entity != null)
        {
            entity.DeletedAt = DateTime.UtcNow;
            await Update(entity);
        }
    }

    public virtual void Dispose(bool disposing)
    {
        if(disposing)
            Db?.Dispose();
    }

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}