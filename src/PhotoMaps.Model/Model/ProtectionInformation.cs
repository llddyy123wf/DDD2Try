using System;

namespace PhotoMaps.Domain
{
	public class ProtectionInformation
	{
		public int Id { get; set; }

		public ValidType ValidType { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }
	}
}