using DDD2Try.EntityFramework.Entity;
using DDD2Try.Infrastructure.Interface;
using DDD2Try.Repository.Interface;

namespace DDD2Try.Repository
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}