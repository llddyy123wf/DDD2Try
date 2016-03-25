using Framework.Infrastructure.Repository;
using PhotoMaps.Domain;
using PhotoMaps.Domain.Repository;
using System.Linq;

namespace PhotoMaps.EntityFramework
{
    public class UserInfoRepository :
        RepositoryBase<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(IDbUnitOfWork unit)
            : base(unit, new UserInfoContext())
        { }


        public IQueryable<UserInfo> UserInfos
        {
            get { return this.Entity<UserInfo>(); }
        }
    }
}