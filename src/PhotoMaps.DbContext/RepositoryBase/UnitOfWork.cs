using Framework.Infrastructure.Repository;

namespace PhotoMaps.EntityFramework
{
    public class UnitOfWork : IDbUnitOfWork
    {
        private IDbContext context;

        public UnitOfWork(IDbContext context)
        {
            this.context = context;
        }

        public void RegisterInsert<T>(T entity) where T : class
        {
            this.context.Insert<T>(entity);
        }

        public void RegisterUpdate<T>(T entity) where T : class
        {
            this.context.Update<T>(entity);
        }

        public void RegisterDelete<T>(T entity) where T : class
        {
            this.context.Delete<T>(entity);
        }

        public void RegisterClean<T>(T entity) where T : class
        {
            this.context.Clean<T>(entity);
        }

        public int Commit()
        {
            return this.context.SaveChanges();
        }

        public void Rollback()
        {
            // auto rollback
        }
    }
}