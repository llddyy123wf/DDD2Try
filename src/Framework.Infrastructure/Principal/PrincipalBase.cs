using System.Security.Principal;

namespace Framework.Infrastructure.Principal
{
    public class PrincipalBase<T> : IPrincipal
        where T : class
    {
        private IdentityBase<T> identity;

        public PrincipalBase(IdentityBase<T> identity)
        {
            this.identity = identity;
        }

        public IIdentity Identity
        {
            get { return this.identity; }
        }

        public bool IsInRole(string role)
        {
            return this.GetInRole(role);
        }

        public T User
        {
            get
            {
                return this.identity.User;
            }
        }

        public virtual bool GetInRole(string role)
        {
            return false;
        }
    }
}