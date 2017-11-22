using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MathTraining.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MathTraining.Data.Core
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity:class
    {
        private readonly MainContext _context;

        private DbSet<TEntity> Set => _context.Set<TEntity>();

        public Repository(MainContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            Set.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            Set.RemoveRange(entities);
        }

        public void Attach(TEntity entity)
        {
            Set.Attach(entity);
        }

        public void Detach(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Detached;

        }

        public TEntity Get(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return Set.Find(id);
        }

        public Task<TEntity> GetAsync(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            return Set.FindAsync(id);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            var localresult = Set.Local.FirstOrDefault(filter.Compile());

            return localresult ?? Set.FirstOrDefault(filter);
        }

        public IQueryable<TEntity> Filtered(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            return Set.Where(filter);
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] include)
        {
            //throw new NotImplementedException();
            IQueryable<TEntity> dbSet = Set;
            return include.Aggregate(dbSet, (current, expression) => current.Include(expression));
        }
    }
}
