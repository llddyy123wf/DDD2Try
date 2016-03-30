using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DDD2Try.EntityFramework.Entity;

namespace DDD2Try.EntityFramework.Configuration
{
    public class CarConfiguration : EntityTypeConfiguration<Car>
    {
        public CarConfiguration()
        {
            ToTable("Car");

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CarsId).IsRequired();
            Property(p => p.CarModelId).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(256);
            Property(p => p.CreateTime).IsRequired();
            Property(p => p.CreateUserId).IsRequired();
        }
    }
}