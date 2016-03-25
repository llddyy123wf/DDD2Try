using System.Configuration;

namespace Framework.Library.Configuration
{
	public static class ConfigurationHelper
	{
		public static T GetConfig<T>(string sectionName)
			where T : new()
		{
			object config = ConfigurationManager.GetSection(sectionName);
			return (T)config;
		}
	}
}
