using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Framework.Library.Mvc
{
    /// <summary>
    /// 角色验证特性
    /// 需要将角色信息存储在Ticket中的UserData中
    /// </summary>
    /// <remarks>
    /// <location path="Admin">
    /// <system.web>
    ///  <authorization>
    ///    <allow roles="Admin" />
    ///    <deny users="?" />
    ///  </authorization>
    /// </system.web>
    /// </location>
    /// </remarks>
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] currentRoles;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("HttpContext");
            if (!httpContext.User.Identity.IsAuthenticated) return false;
            if (currentRoles == null) return true;
            if (currentRoles.Length == 0) return true;

            string userRole = ((FormsIdentity)httpContext.User.Identity).Ticket.UserData;
            if (currentRoles.Contains(userRole)) return true;

            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            List<string> roles = new List<string>();
            AuthorizationSection section = filterContext.HttpContext.GetSection("system.web/authorization") as AuthorizationSection;
            foreach (AuthorizationRule rule in section.Rules)
            {
                if (rule.Roles.Count > 0 && (rule.Verbs.Count == 0 || rule.Verbs.Contains(filterContext.HttpContext.Request.HttpMethod)))
                {
                    foreach (var role in rule.Roles)
                    {
                        if (rule.Action == AuthorizationRuleAction.Allow)
                        {
                            roles.Add(role);
                        }
                        else
                        {
                            roles.Remove(role);
                        }
                    }
                }
            }
            this.currentRoles = roles.ToArray();

            base.OnAuthorization(filterContext);
        }
    }
}
