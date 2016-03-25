using Framework.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhotoMaps.Dapper
{
    public class RepositoryBase<T, TContext>
        where TContext : DbContext
        where T : class
    {
        protected TContext Context { get; set; }
        public RepositoryBase(TContext context)
        {
            this.Context = context;
        }

        public T LoadByKey<TKey>(TKey key)
        {
            return this.Context.LoadByKey<T,TKey>(key);
        }

        public void Save(T entity)
        {
            this.Context.Insert<T>(entity);
        }
        public void Save(IEnumerable<T> entities)
        {
            this.Context.Insert<T>(entities);
        }
        public void Delete(T entity)
        {
            this.Context.Delete<T>(entity);
        }
        public void Update(T entity)
        {
            this.Context.Update<T>(entity);
        }

        public bool Delete(Expression<Func<T, bool>> filter)
        {
            return this.Context.Delete<T>(filter);
        }
        public bool Update(Expression<Func<T, bool>> filter, Expression<Func<T, T>> update)
        {
            return this.Context.Update<T>(filter, update);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return this.Context.ExecuteSqlCommand(sql, parameters);
        }
        public IEnumerable<T> ExcuteSql(string sql, params object[] parameters)
        {
            return this.Context.ExcuteSql<T>(sql, parameters);
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
            if (disposing) {
                this.Context.Dispose();
            }

            disposed = true;
        }
        ~RepositoryBase()
        {
            Dispose(false);
        }
    }
}