using System.Linq.Expressions;

namespace TradingAutomaticoFx_Net.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        TEntity? Find(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> predicate, string includeProperitesString,
            params Expression<Func<TEntity, object>>[] includeProperties);

        void Delete(TEntity entity);
        void Delete(object id);
        void DeleteRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null);

        TEntity? GetById(params object[] keyValues);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Refresh(TEntity entity);

        string? GetDataReturnOneColumn(
          Expression<Func<TEntity, string>> column,
          Expression<Func<TEntity, bool>>? filter = null
        );

        void SaveChanges();
    }
}
