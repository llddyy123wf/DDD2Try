using System;
using System.Data.Entity;
using DDD2Try.Infrastructure.Interface;

namespace DDD2Try.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void RegisterDirty<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void RegisterClean<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;
        }

        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public bool Rollback()
        {
            //只有在非EF框架此方法才有用
            throw new NotImplementedException();
        }
    }
}