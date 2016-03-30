using System.Linq;

namespace DDD2Try.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
    }
}