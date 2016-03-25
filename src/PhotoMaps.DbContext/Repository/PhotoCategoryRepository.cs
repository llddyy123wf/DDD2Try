using Framework.Infrastructure.Repository;
using PhotoMaps.Domain;
using PhotoMaps.Domain.Repository;
using System.Linq;

namespace PhotoMaps.EntityFramework
{
    public class PhotoCategoryRepository :
        RepositoryBase<PhotoCategory>, IPhotoCategoryRepository
    {
        public PhotoCategoryRepository(IDbUnitOfWork unit)
            : base(unit, new PhotoCategoryContext())
        { }


        public IQueryable<PhotoCategory> Categories
        {
            get { return this.Entity<PhotoCategory>(); }
        }
    }
}