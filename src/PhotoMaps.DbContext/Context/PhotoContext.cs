using Framework.Infrastructure.Repository;
using PhotoMaps.Domain;
using PhotoMaps.EntityFramework.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace PhotoMaps.EntityFramework
{
    /// <summary>
    /// 数据操作上下文
    /// 负责与数据库进行交互操作
    /// </summary>
    [DbConfigurationType(typeof(DataConfiguration))]
    public class PhotoContext : DbContext, IDbContext
    {
        public PhotoContext()
            : base("PhotoMapsConn")
        {
        }

        /// <summary>
        /// 使用Fluent API对数据配置进行操作
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 设置统一属性
            // 设置string映射长度为64
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(64));

            // 设置一般类型
            modelBuilder.Configurations.Add(new PhotoConfiguration());
            modelBuilder.Configurations.Add(new PhotoCategoryConfiguration());
            modelBuilder.Configurations.Add(new PhotoInformationConfiguration());
            modelBuilder.Configurations.Add(new ProtectionInformationConfiguration());
            // 配置复杂类型1
            modelBuilder.Configurations.Add<PhotoCoordinate>(new PhotoCoordinateConfiguration());

            // 配置概念模型 System.Data.Entity.ModelConfiguration.Conventions
            // 优先级Fluent API > 数据注释 > 约定

            // 移除约定
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // 级联删除
            // 外键不可空, 默认启用级联删除
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Context相关配置
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
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