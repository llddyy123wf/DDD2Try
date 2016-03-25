using System.Collections.Generic;
using System.Linq;

namespace Framework.Infrastructure.Repository
{
    public interface IDbContext
    {
        IQueryable<TEntity> Entity<TEntity>() where TEntity : class;

        TEntity LoadByKey<TEntity, TKey>(TKey key) where TEntity : class;

        void Insert<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        void Clean<T>(T entity) where T : class;

        int ExecuteSqlCommand(string sql, params object[] parameters);

        IEnumerable<T> ExcuteSql<T>(string sql, params object[] parameters) where T : class;

        int SaveChanges();
    }
}