using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TradingAutomaticoFx_Net.Infrastructure.Context;
using TradingAutomaticoFx_Net.Infrastructure.Repositories.Interfaces;

namespace TradingAutomaticoFx_Net.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly ApplicationDbcontext _context;

        public GenericRepository(ApplicationDbcontext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TEntity? GetById(params object[] keyValues)
        {
            return _context.Set<TEntity>().Find(keyValues);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public TEntity? Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public void Delete(object id)
        {
            TEntity? entityToDelete = GetById(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
        }

        public void Refresh(TEntity entity)
        {
            _context.Entry(entity).Reload();
        }

        public string? GetDataReturnOneColumn(Expression<Func<TEntity, string>> column,
            Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Select(column).FirstOrDefault();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindByIncluding(predicate, string.Empty, includeProperties);
        }

        public IQueryable<TEntity> FindByIncluding(Expression<Func<TEntity, bool>> predicate, string includeProperitesString,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (!string.IsNullOrEmpty(includeProperitesString))
            {
                query = query.Include(includeProperitesString);
            }

            return query.Where(predicate);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
