using PhotoMaps.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMaps.EntityFramework.Configuration
{
    public class UserInfoConfiguration : EntityTypeConfiguration<UserInfo>
    {
        public UserInfoConfiguration()
        {
            ToTable("UserInfo");

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.UserName).IsRequired().HasMaxLength(36);
            Property(p => p.Password).IsRequired().HasMaxLength(256);
            Property(p => p.CreateTime).IsRequired();
            Property(p => p.UpdateTime).IsRequired();
            Property(p => p.DisplayName).IsRequired().HasMaxLength(36);
        }
    }
}