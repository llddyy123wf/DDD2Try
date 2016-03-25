using DapperExtensions.Mapper;
using PhotoMaps.Domain;

namespace PhotoMaps.Dapper.Configuration
{
    public class UserInfoMapper : ClassMapper<UserInfo>
    {
        public UserInfoMapper()
        {
            Table("UserInfo");
            //Schema("");
            Map(x => x.Id).Key(KeyType.Identity);
            Map(x => x.CreateTime).Column("CreateTime");
            AutoMap();
        }
    }
}
