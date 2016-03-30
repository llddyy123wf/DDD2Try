using DDD2Try.EntityFramework.Entity;
using DDD2Try.Infrastructure.Interface;
using DDD2Try.Repository.Interface;

namespace DDD2Try.Repository
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(IDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}