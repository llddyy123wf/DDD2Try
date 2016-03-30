using System.Data.Entity;
using DDD2Try.EntityFramework.Configuration;
using DDD2Try.Infrastructure.Interface;

namespace DDD2Try.Infrastructure
{
    public class MasterDbContext : DbContext, IDbContext
    {
        public MasterDbContext() : base("MasterDB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserInfoConfiguration());
            modelBuilder.Configurations.Add(new CarConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new Product_CarConfiguration());
            modelBuilder.Configurations.Add(new RunRecordConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}