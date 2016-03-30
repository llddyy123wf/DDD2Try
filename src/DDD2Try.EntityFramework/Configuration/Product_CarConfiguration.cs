using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DDD2Try.EntityFramework.Entity;

namespace DDD2Try.EntityFramework.Configuration
{
    public class Product_CarConfiguration : EntityTypeConfiguration<Product_Car>
    {
        public Product_CarConfiguration()
        {
            ToTable("Product_Car");

            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.IsEffective).IsRequired();
            Property(p => p.CreateUserId).IsRequired();
            Property(p => p.CreateTime).IsRequired();
            Property(p => p.ProductId).IsRequired();
            Property(p => p.CarId).IsRequired();
        }
    }
}