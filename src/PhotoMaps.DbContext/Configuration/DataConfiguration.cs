using System.Data.Entity;

namespace PhotoMaps.EntityFramework.Configuration
{
    public class DataConfiguration : DbConfiguration
    {
        public DataConfiguration()
        {
            // 关闭策略
            this.SetDatabaseInitializer<PhotoContext>(null);
            this.SetDatabaseInitializer<UserInfoContext>(null);

            // 初始化策略
            // 可以使用自定义策略或配置文件中配置
            // Database.SetInitializer<PhotoContext>(new CreateDatabaseIfNotExists<PhotoContext>());

            // 设置策略, 重试次数等等
            // this.SetExecutionStrategy()

            // 约定
            // SetPluralizationService(null);
        }
    }
}