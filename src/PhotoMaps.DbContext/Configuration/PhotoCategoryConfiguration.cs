using PhotoMaps.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMaps.EntityFramework.Configuration
{
    public class PhotoCategoryConfiguration : EntityTypeConfiguration<PhotoCategory>
    {
        public PhotoCategoryConfiguration()
        {
            ToTable("PhotoCategory");

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).IsRequired().HasMaxLength(16);
            Property(p => p.Description).IsRequired().HasMaxLength(36);
        }
    }
}