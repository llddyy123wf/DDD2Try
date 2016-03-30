using Framework.Library.Security;
using System;

namespace DDD2Try.Domain
{
    public static class UserInfoSpecification
    {
        public static string HashPassword(this UserInfo user, string password)
        {
            string hashPassword = (user.UpdateTime.Ticks.ToString() + password).HashBySHA256();
            return hashPassword;
        }

        public static void Initial(this UserInfo user)
        {
            user.CreateTime = DateTime.Now;
            user.UpdateTime = user.CreateTime;
            user.Status = UserStatus.Activated;
            user.Password = user.HashPassword(user.Password);
        }

        public static bool ComparePassword(this UserInfo user, string password)
        {
            string hashPassword = user.HashPassword(password);
            return user.Password == hashPassword;
        }
    }
}
