using PhotoMaps.Dapper.Configuration;
using PhotoMaps.Domain;

namespace PhotoMaps.Dapper
{
    public class UserInfoContext : DbContext
    {
        public UserInfoContext()
            : base()
        {
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Mappers.Add(new UserInfoMapper());
        }
    }
}
