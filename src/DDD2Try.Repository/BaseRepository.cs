using System.Linq;
using DDD2Try.Infrastructure.Interface;
using DDD2Try.Repository.Interface;

namespace DDD2Try.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly IQueryable<T> _entities;

        public BaseRepository(IDbContext dbContext)
        {
            _entities = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }
    }
}