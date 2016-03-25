using PhotoMaps.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMaps.EntityFramework.Configuration
{
    public class PhotoInformationConfiguration : EntityTypeConfiguration<PhotoInformation>
    {
        public PhotoInformationConfiguration()
        {
            ToTable("PhotoInformation");

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Address).IsOptional().HasMaxLength(64);
            Property(p => p.Camera).IsOptional().HasMaxLength(64);
            Property(p => p.Extend).IsOptional().HasMaxLength(1024);
            Property(p => p.TakeTime).IsOptional();
        }
    }
}