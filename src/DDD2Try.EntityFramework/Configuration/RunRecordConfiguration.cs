using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DDD2Try.EntityFramework.Entity;

namespace DDD2Try.EntityFramework.Configuration
{
    public class RunRecordConfiguration : EntityTypeConfiguration<RunRecord>
    {
        public RunRecordConfiguration()
        {
            ToTable("RunRecord");

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Product_CarId).IsRequired();
            Property(p => p.CreateUserId).IsRequired();
            Property(p => p.CreateTime).IsRequired();
            Property(p => p.StartTime).IsRequired();
            Property(p => p.EndTime).IsRequired();
            Property(p => p.StartLatitude).HasPrecision(10, 6);
            Property(p => p.EndLatitude).HasPrecision(10, 6);
            Property(p => p.StartLongitude).HasPrecision(10, 6);
            Property(p => p.EndLongitude).HasPrecision(10, 6);
            Property(p => p.StartMileage).HasPrecision(18, 3);
            Property(p => p.EndMileage).HasPrecision(18, 3);
            Property(p => p.AverageSpeed).HasPrecision(18, 3);
            Property(p => p.MaxSpeed).HasPrecision(18, 3);
        }
    }
}