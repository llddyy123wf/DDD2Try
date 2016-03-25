using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PhotoMaps.EntityFramework
{
    public static class RepositoryExtension
    {
        public static void Save<T>(this DbContext context, T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException("entity");

            EntityState state = context.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                context.Entry(entity).State = EntityState.Added;
            }
        }
        public static void Save<T>(this DbContext context, IEnumerable<T> entities) where T : class
        {
            if (entities == null) throw new ArgumentNullException("entities");

            foreach (T entity in entities)
            {
                context.Save(entity);
            }
        }
        public static void Delete<T>(this DbContext context, T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException("entity");
            context.Entry(entity).State = EntityState.Deleted;
        }
        public static void Update<T>(this DbContext context, T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException("entity");
            context.Entry(entity).State = EntityState.Modified;
        }

        public static int Delete<T>(this DbContext context, Expression<Func<T, bool>> filter) where T : class
        {
            return BatchExtensions.Delete(context.Set<T>().Where(filter));
        }
        public static int Update<T>(this DbContext context, Expression<Func<T, bool>> filter, Expression<Func<T, T>> update) where T : class
        {
            return BatchExtensions.Update(context.Set<T>().Where(filter), update);
        }

        public static int ExecuteSqlCommand(this DbContext context, string sql, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction, sql, parameters);
        }
        public static IEnumerable<T> ExcuteSql<T>(this DbContext context, string sql, params object[] parameters) where T : class
        {
            return context.Set<T>().SqlQuery(sql, parameters).AsEnumerable<T>();
        }
    }
}
