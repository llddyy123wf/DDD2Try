using System;

namespace PhotoMaps.Domain
{
	public class UserInfo
	{
		public int Id { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

        public string DisplayName { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

		public UserStatus Status { get; set; }
	}
}