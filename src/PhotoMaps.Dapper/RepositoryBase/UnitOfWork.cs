using Framework.Infrastructure.Repository;
using System;

namespace PhotoMaps.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private IContext context;

        public UnitOfWork(IContext context)
        {
            this.context = context;
            this.context.BeginTransaction();
        }


        public void Rollback()
        {
            if (context.IsTransactionStarted)
            {
                context.Rollback();
            }
        }

        public void SaveChanges()
        {
            if (!context.IsTransactionStarted)
                throw new InvalidOperationException("Transaction have already been commited or disposed.");

            context.Commit();
        }
    }
}
