using System.Collections.Generic;
using System.Linq;

namespace Framework.Infrastructure.Repository
{
    public interface IContext
    {
        bool IsTransactionStarted { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}