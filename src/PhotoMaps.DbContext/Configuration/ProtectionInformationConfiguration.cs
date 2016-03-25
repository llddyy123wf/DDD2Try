using PhotoMaps.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMaps.EntityFramework.Configuration
{
    public class ProtectionInformationConfiguration : EntityTypeConfiguration<ProtectionInformation>
    {
        public ProtectionInformationConfiguration()
        {
            ToTable("ProtectionInformation");

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Question).IsRequired().HasMaxLength(64);
            Property(p => p.Answer).IsRequired().HasMaxLength(64);
        }
    }
}