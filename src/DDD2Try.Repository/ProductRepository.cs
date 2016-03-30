using DDD2Try.EntityFramework.Entity;
using DDD2Try.Infrastructure.Interface;
using DDD2Try.Repository.Interface;

namespace DDD2Try.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}