using Framework.Infrastructure.Repository;
using PhotoMaps.Domain;
using PhotoMaps.Domain.Repository;
using System.Linq;

namespace PhotoMaps.EntityFramework
{
    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(IDbUnitOfWork unit)
            : base(unit,new PhotoContext())
        { }

        public IQueryable<Photo> Photos
        {
            get { return this.Entity<Photo>(); }
        }

        public IQueryable<UserInfo> Users
        {
            get { return this.Entity<UserInfo>(); }
        }

        public IQueryable<PhotoCategory> Categories
        {
            get { return this.Entity<PhotoCategory>(); }
        }
    }
}