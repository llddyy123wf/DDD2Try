using Framework.Infrastructure.Repository;
using System.Linq;

namespace PhotoMaps.Domain.Repository
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        IQueryable<UserInfo> UserInfos { get; }
    }
}
