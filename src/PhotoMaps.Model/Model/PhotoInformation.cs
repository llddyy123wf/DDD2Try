using System;

namespace PhotoMaps.Domain
{
	public class PhotoInformation
	{
        public int Id { get; set; }

		public string Camera { get; set; }

        public DateTime TakeTime { get; set; }

		public string Address { get; set; }

        public string Extend { get; set; }
	}
}