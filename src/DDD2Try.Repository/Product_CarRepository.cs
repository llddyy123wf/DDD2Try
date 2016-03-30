using DDD2Try.EntityFramework.Entity;
using DDD2Try.Infrastructure.Interface;
using DDD2Try.Repository.Interface;

namespace DDD2Try.Repository
{
    public class Product_CarRepository : BaseRepository<Product_Car>, IProduct_CarRepository
    {
        public Product_CarRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}