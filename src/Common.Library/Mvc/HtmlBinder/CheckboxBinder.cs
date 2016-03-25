using System;
using System.Linq;
using System.Web;

namespace Framework.Library.Mvc
{
    public class CheckboxBinder
    {
        public static T Bind<T>(string property) where T : struct
        {
            string[] properties = HttpContext.Current.Request.Form[property].Split(',');
            int enumValue = properties.Sum(p => int.Parse(p));
            return (T)Enum.Parse(typeof(T), enumValue.ToString());
        }

        public static bool Bind(string property)
        {
            string hasProperty = HttpContext.Current.Request.Form[property];
            return (hasProperty == "on" ? true : false);
        }
    }
}
