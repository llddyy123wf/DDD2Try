using System.Configuration;

namespace Framework.Library.Configuration
{
	/// <summary>
	/// 以名字为键值的配置项
	/// </summary>
	public class NamedConfigurationElement : ConfigurationElement
	{
		/// <summary>
		/// 配置名称
		/// </summary>
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public virtual string Name
		{
			get
			{
				return (string)this["name"];
			}
			set
			{
				this["name"] = value;
			}
		}

		/// <summary>
		/// 配置描述
		/// </summary>
		[ConfigurationProperty("description", DefaultValue = "")]
		public virtual string Description
		{
			get
			{
				return (string)this["description"];
			}
			set
			{
				this["description"] = value;
			}
		}
	}

	/// <summary>
	/// 以名字为键值的配置项集合
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class NamedConfigurationElementCollection<T> : ConfigurationElementCollection
		where T : NamedConfigurationElement, new()
	{
		#region Property
		/// <summary>
		/// 集合中元素数量
		/// </summary>
		public new int Count
		{
			get { return base.Count; }
		}

		/// <summary>
		/// 按照序号获取或设置配置元素
		/// </summary>
		/// <param name="index">序号</param>
		/// <returns>配置元素</returns>
		public T this[int index]
		{
			get
			{
				return (T)BaseGet(index);
			}
			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}
				BaseAdd(index, value);
			}
		}

		#endregion

		#region Protected Method
		/// <summary>
		/// 生成新的配置元素实例
		/// </summary>
		/// <returns>配置元素实例</returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new T();
		}

		/// <summary>
		/// 得到元素对应的Key值
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((T)element).Name;
		}
		#endregion
	}
}
