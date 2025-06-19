﻿using e_Estoque_API.Core.Models;
using e_Estoque_API.Domain.Entities;
using System.Linq.Expressions;

namespace e_Estoque_API.Core.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
{
    Task AddAsync(TEntity entity);

    Task<TEntity?> GetByIdAsync(Guid id);

    IQueryable<TEntity> GetAllQueryable();

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task UpdateAsync(TEntity entity);

    Task RemoveAsync(Guid id);
    Task DisableAsync(Guid id);
    Task ActiveAsync(Guid id);
    Task ActiveOrDisableAsync(Guid id, bool active);

    Task<BaseResultList<TEntity>> SearchAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageSize = 10, int page = 1);

    Task<BaseResultList<TEntity>> SearchAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int pageSize = 10, int page = 1);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}
