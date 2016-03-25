using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        T LoadByKey<TKey>(TKey key);

        void Save(T entity);

        void Save(IEnumerable<T> entities);

        void Delete(T entity);

        void Update(T entity);

        int Delete(Expression<Func<T, bool>> filter);

        int Update(Expression<Func<T, bool>> filter, Expression<Func<T, T>> update);

        int ExecuteSqlCommand(string sql, params object[] parameters);

        IEnumerable<T> ExcuteSql(string sql, params object[] parameters);
    }
}