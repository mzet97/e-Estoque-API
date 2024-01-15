using e_Estoque_API.Core.Entities;
using e_Estoque_API.Core.Models;
using System.Linq.Expressions;

namespace e_Estoque_API.Core.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : AggregateRoot
    {
        Task Add(TEntity entity);
        Task<TEntity?> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task Update(TEntity entity);
        Task Remove(Guid id);
        Task<BaseResult<TEntity>> Search(
             Expression<Func<TEntity, bool>>? predicate = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
             int pageSize = 10, int page = 1);
    }
}
