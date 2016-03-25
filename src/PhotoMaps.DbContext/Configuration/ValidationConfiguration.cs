using PhotoMaps.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMaps.EntityFramework.Configuration
{
    public class ValidationConfiguration : EntityTypeConfiguration<Validation>
    {
        public ValidationConfiguration()
        {
            ToTable("Validation", ProjectConfiguration.PROJECT_SCHEMA);

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.CreateTime).IsRequired();
            Property(p => p.ExpiredTime).IsRequired();
            Property(p => p.ResourceId).IsRequired();
            Property(p => p.ValidCode).IsRequired();
        }
    }
}