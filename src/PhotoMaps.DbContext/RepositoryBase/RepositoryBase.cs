using EntityFramework.Extensions;
using Framework.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhotoMaps.EntityFramework
{
    public class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        private IDbUnitOfWork unit;
        private IDbContext context;

        public RepositoryBase(IDbUnitOfWork unit, IDbContext context)
        {
            this.context = context;
            this.unit = unit;
        }

        protected IQueryable<TEntity> Entity<TEntity>() where TEntity : class
        {
            return this.context.Entity<TEntity>();
        }

        public T LoadByKey<TKey>(TKey key)
        {
            return this.context.LoadByKey<T, TKey>(key);
        }

        public void Save(T entity)
        {
            this.unit.RegisterInsert<T>(entity);
        }

        public void Save(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.unit.RegisterInsert(entity);
            }
        }

        public void Delete(T entity)
        {
            this.unit.RegisterDelete<T>(entity);
        }

        public void Update(T entity)
        {
            this.unit.RegisterUpdate<T>(entity);
        }

        public int Delete(Expression<Func<T, bool>> filter)
        {
            int changes = BatchExtensions.Delete(this.context.Entity<T>().Where(filter));
            return changes;
        }

        public int Update(Expression<Func<T, bool>> filter, Expression<Func<T, T>> update)
        {
            int changes = BatchExtensions.Update(this.context.Entity<T>().Where(filter), update);
            return changes;
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return this.context.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<T> ExcuteSql(string sql, params object[] parameters)
        {
            return this.context.ExcuteSql<T>(sql, parameters);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) { }

            disposed = true;
        }

        ~RepositoryBase()
        {
            Dispose(false);
        }
    }
}