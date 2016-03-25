using PhotoMaps.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMaps.EntityFramework
{
    public class PhotoConfiguration : EntityTypeConfiguration<Photo>
    {
        public PhotoConfiguration()
        {
            // 数据库及Schema
            ToTable("Photo");

            // 主键
            HasKey(p => p.Id);
            // 可设置主键的生成方式(Identity: 自增长)
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).IsRequired().HasMaxLength(16);
            Property(p => p.Description).IsRequired().HasMaxLength(64);
            Property(p => p.PhotoUrl).IsRequired().HasMaxLength(256);
            Property(p => p.Access).IsRequired();
            Property(p => p.CreateTime).IsRequired();

            // 配置复杂类型2
            // 复杂类型在具体类型的配置(建议)
            Property(p => p.Coordinate.Latitude).HasColumnName("Latitude");
            Property(p => p.Coordinate.Longitude).HasColumnName("Longitude");

            // 单向集合配置(1-0..*)
            // Map命名模型中未定义的外键, HasForeignKey指定已定义外键
            HasMany(p => p.Protections)
                .WithOptional()
                // .HasForeignKey<string>(m=>m.PhotoId)
                .Map(m => m.MapKey("PhotoId"))
                // 设置不启用级联删除
                .WillCascadeOnDelete(false);

            // 单向配置(1-0..1), 存在A, 不一定存在B, 存在B必须有A
            HasOptional(p => p.Information).WithRequired();
            // 单向属性导航(0..1-1), 存在B, 不一定存在A, INNER JOIN
            // .HasRequired(p => p.Information).WithOptional();
            // 单向属性导航(1-1), 同时存在, B是Principle, A是Dependent(不能单独持久化), INNER JOIN
            // .HasRequired(p=>p.Information).WithRequiredDependent();
            // 单向属性导航(1-1), 同时存在, B是Dependent, A是Principle(允许单独持久化), INNER JOIN
            // .HasRequired(p=>p.Information).WithRequiredPrincipal(), LEFT JOIN

            // 单向属性导航(0..*-1), A有B的外键UserId
            HasRequired(p => p.Author)
                .WithMany()
                .Map(m => m.MapKey("UserId"))
                .WillCascadeOnDelete(false);

            // 单向集合配置(0..*-0..*)
            HasMany(p => p.Categories).WithMany().Map(m =>
                {
                    m.ToTable("Photo_PhotoCategory");
                    m.MapLeftKey("PhotoId");
                    m.MapRightKey("CategoryId");
                });
        }
    }
}