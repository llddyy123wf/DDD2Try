using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace Framework.Library
{
    public static class EnumHelper
    {
        /// <summary>
        /// 根据枚举类型返回所有的值及描述信息集合
        /// </summary>
        /// <param name="enumType">枚举</param>
        /// <returns>枚举描述集合</returns>
        public static NameValueCollection GetDescriptions(Enum enumeration)
        {
            NameValueCollection nvs = new NameValueCollection();

            Type enumType = enumeration.GetType();
            FieldInfo[] fields = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);

            Type descAttr = typeof(DescriptionAttribute);
            foreach (FieldInfo field in fields)
            {
                string itemValue = ((int)field.GetValue(null)).ToString();
                object[] attributes = field.GetCustomAttributes(descAttr, true);
                if (attributes.Length > 0)
                {
                    string itemDesc = ((DescriptionAttribute)attributes[0]).Description;
                    nvs.Add(itemValue, itemDesc);
                }
            }
            return nvs;
        }

        /// <summary>
        /// 根据枚举项获得相应的描述
        /// </summary>
        /// <param name="enumItem">枚举</param>
        /// <returns>枚举描述</returns>
        public static string GetDescription(this Enum enumeration)
        {
            Type enumType = enumeration.GetType();
            bool hasFlag = enumType.GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;

            Type descAttr = typeof(DescriptionAttribute);
            FieldInfo[] fields = enumeration.GetType().GetFields(BindingFlags.Static | BindingFlags.Public);

            string separtor = ",";
            string itemDesc = string.Empty;
            foreach (FieldInfo field in fields)
            {
                int enumValue = ((int)field.GetValue(null));
                int sourceValue = Convert.ToInt32(enumeration);
                
                bool hasValue = (hasFlag && (sourceValue & enumValue) == enumValue) || enumValue == sourceValue;

                if (hasValue)
                {
                    object[] attributes = field.GetCustomAttributes(descAttr, true);
                    if (attributes.Length > 0)
                    {
                        itemDesc += ((DescriptionAttribute)attributes[0]).Description + separtor;
                    }
                }
            }
            if (itemDesc.Length > 0) return itemDesc.Remove(itemDesc.Length - 1, 1);
            return itemDesc;
        }
    }
}
