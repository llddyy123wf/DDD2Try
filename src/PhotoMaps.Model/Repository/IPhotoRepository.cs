using Framework.Infrastructure.Repository;
using System.Linq;

namespace PhotoMaps.Domain.Repository
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        IQueryable<Photo> Photos { get; }
        IQueryable<UserInfo> Users { get; }
        IQueryable<PhotoCategory> Categories { get; }
    }
}
