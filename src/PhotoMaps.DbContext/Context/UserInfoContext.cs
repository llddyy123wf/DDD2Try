using Framework.Infrastructure.Repository;
using PhotoMaps.EntityFramework.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PhotoMaps.EntityFramework
{
    [DbConfigurationType(typeof(DataConfiguration))]
    public class UserInfoContext : DbContext, IDbContext
    {
        public UserInfoContext()
            : base("PhotoMapsConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserInfoConfiguration());
        }

        public IQueryable<TEntity> Entity<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>().AsQueryable<TEntity>();
        }

        public TEntity LoadByKey<TEntity, TKey>(TKey key) where TEntity : class
        {
            return this.Set<TEntity>().Find(key);
        }

        public void Insert<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException("entity");

            EntityState state = this.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                this.Entry(entity).State = EntityState.Added;
            }
        }

        public void Update<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException("entity");
            this.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException("entity");
            this.Entry(entity).State = EntityState.Deleted;
        }

        public void Clean<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException("entity");
            this.Entry(entity).State = EntityState.Unchanged;
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return this.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction, sql, parameters);
        }

        public IEnumerable<T> ExcuteSql<T>(string sql, params object[] parameters) where T : class
        {
            return this.Set<T>().SqlQuery(sql, parameters).AsEnumerable<T>();
        }
    }
}