using PhotoMaps.Domain;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMaps.EntityFramework.Configuration
{
    public class PhotoCoordinateConfiguration : ComplexTypeConfiguration<PhotoCoordinate>
    {
        public PhotoCoordinateConfiguration()
        {
            // 复杂类型配置1.1
            // 复杂类型的公共配置(建议)
            this.Property(p => p.Latitude).HasPrecision(10, 3);
            this.Property(p => p.Longitude).HasPrecision(10, 3);
        }
    }
}