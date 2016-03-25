using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Library
{
    /// <summary>
    /// 通用扩展方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T TryParse<T>(this object value, T defaultValue = default(T))
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = defaultValue;
            }
            return (T)result;
        }
    }
}
