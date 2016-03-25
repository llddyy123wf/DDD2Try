using Framework.Infrastructure.Repository;
using System.Linq;

namespace PhotoMaps.Domain.Repository
{
    public interface IPhotoCategoryRepository : IRepository<PhotoCategory>
    {
        IQueryable<PhotoCategory> Categories { get; }
    }
}
