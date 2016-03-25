using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;

namespace Framework.Library.Web
{
    public static class HttpContextExtensions
    {
        public static string[] GetAuthorizationRole(this HttpContextBase context)
        {
            List<string> roles = new List<string>();
            AuthorizationSection section = context.GetSection("system.web/authorization") as AuthorizationSection;
            foreach (AuthorizationRule rule in section.Rules)
            {
                if (rule.Roles.Count > 0)
                {
                    if (rule.Verbs.Count == 0 || rule.Verbs.Contains(context.Request.HttpMethod))
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
            }
            return roles.ToArray();
        }
    }
}
