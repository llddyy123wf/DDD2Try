using Framework.Infrastructure.Repository;
using PhotoMaps.Domain;
using PhotoMaps.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMaps.Application
{
    public class PhotoApplication
    {
        private IPhotoRepository repository;
        private IUnitOfWork unitOfWork;

        public PhotoApplication(IPhotoRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Photo> LoadByCoordinates(PhotoCoordinate swCoordinate, PhotoCoordinate neCoordinate, int level)
        {
            return this.repository.Photos.Where(p => p.Coordinate.Latitude > swCoordinate.Latitude &&
                                                     p.Coordinate.Longitude > swCoordinate.Longitude &&
                                                     p.Coordinate.Latitude < neCoordinate.Latitude &&
                                                     p.Coordinate.Longitude < neCoordinate.Longitude)
                                         .ToList();
        }

        public void Save(Photo photo)
        {
            this.repository.Save(photo);
            this.unitOfWork.Commit();
        }
    }
}