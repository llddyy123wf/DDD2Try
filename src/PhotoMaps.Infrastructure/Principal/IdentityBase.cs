using System.Security.Principal;
using System.Web;

namespace Framework.Infrastructure.Principal
{
    public abstract class IdentityBase<T> : IIdentity
        where T : class
    {
        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public string Name
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }

        public T User
        {
            get
            {
                return this.GetUser();
            }
        }

        protected abstract T GetUser();
    }
}