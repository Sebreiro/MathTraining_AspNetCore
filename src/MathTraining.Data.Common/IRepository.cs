using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MathTraining.Data.Common
{
    public interface IRepository<TEntity> where TEntity:class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Attach(TEntity entity);
        void Detach(TEntity entity);
        TEntity Get(object id);
        Task<TEntity> GetAsync(object id);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> Filtered(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] include);

    }
}
