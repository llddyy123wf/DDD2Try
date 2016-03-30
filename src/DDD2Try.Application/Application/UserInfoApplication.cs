using DDD2Try.Domain;
using DDD2Try.Domain.IRepository;

namespace DDD2Try.Application
{
    public class UserInfoApplication
    {
        public static readonly UserInfoApplication Instance = ApplicationFactory.Create<UserInfoApplication>();
        private readonly IUserInfoRepository repository;

        public UserInfoApplication(IUserInfoRepository repository)
        {
            this.repository = repository;
        }

        public MessageResult<UserInfo> Login(string userName, string password)
        {
            UserInfo user = repository.UserInfos.Where(p => p.UserName == userName).SingleOrDefault();

            MessageResult<UserInfo> result = new MessageResult<UserInfo>();
            if (user == null || user.Status == UserStatus.Cancelled)
            {
                result.Success = false;
                result.Message = "无效的用户名或密码";
                return result;
            }

            if (user.Status == UserStatus.Cancelled)
            {
                result.Success = false;
                result.Message = "用户被锁定";
                return result;
            }

            // hash密码
            user.Password = user.HashPassword(user.Password);
            if (user.Password == password)
            {
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.Message = "无效的密码或密码";
            }

            return result;
        }

        public MessageResult<string> Register(UserInfo user)
        {
            int userCount = repository.UserInfos.Where(p => p.UserName == user.UserName).Select(p => p.Id).Count();

            MessageResult<string> result = new MessageResult<string>();
            if (userCount > 0)
            {
                // 注册
                user.Initial();
                repository.Save(user);
                result.Success = true;
            }
            else
            {
                result.Success = false;
                result.Message = "该用户已存在";
            }

            return result;
        }
    }
}