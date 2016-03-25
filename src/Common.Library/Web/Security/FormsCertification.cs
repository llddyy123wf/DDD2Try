using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Framework.Library.Web.Security
{
    public class FormsCertification
    {
        /// <summary>
        /// 创建认证ticket, 并写入cookie
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="userData">用户数据</param>
        /// <param name="expiredMinutes">过期日期, 0则使用默认配置</param>
        public static void SetAuthCookie(string userName, string userData, bool isPersistent = true)
        {
            DateTime nowDate = DateTime.Now;
            DateTime expiredDate = nowDate.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, userName, nowDate, expiredDate, isPersistent, userData, FormsAuthentication.FormsCookiePath);
            string strTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strTicket);
            cookie.HttpOnly = true;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 获取认证ticket
        /// </summary>
        /// <returns>认证ticket</returns>
        public static FormsAuthenticationTicket GetAuthCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            return ticket;
        }

        /// <summary>
        /// 重定向登陆前页面
        /// </summary>
        /// <param name="returnUrl">指定返回页面</param>
        public static void RedirectFromLoginPage(string returnUrl = "")
        {
            HttpContext context = HttpContext.Current;

            string strUrl = string.IsNullOrEmpty(returnUrl) ? context.Request.Params["ReturnUrl"] : returnUrl;
            if (string.IsNullOrEmpty(strUrl)) strUrl = FormsAuthentication.DefaultUrl;
            if (string.IsNullOrEmpty(strUrl)) throw new ArgumentNullException("invalid ReturnUrl");

            returnUrl = HttpUtility.UrlDecode(strUrl);
            context.Response.Redirect(strUrl);
        }

        /// <summary>
        /// 注销
        /// </summary>
        public static void SignOut()
        {
            string cookieValue = "NoCookie";
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieValue);
            cookie.HttpOnly = true;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            cookie.Expires = new System.DateTime(1999, 10, 12);

            HttpContext context = HttpContext.Current;
            context.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            context.Response.Cookies.Add(cookie);
        }
    }
}
