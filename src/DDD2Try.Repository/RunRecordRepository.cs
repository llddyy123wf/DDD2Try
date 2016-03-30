using DDD2Try.EntityFramework.Entity;
using DDD2Try.Infrastructure.Interface;
using DDD2Try.Repository.Interface;

namespace DDD2Try.Repository
{
    public class RunRecordRepository : BaseRepository<RunRecord>, IRunRecordRepository
    {
        public RunRecordRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}