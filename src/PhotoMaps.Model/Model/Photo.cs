using System;
using System.Collections.Generic;

namespace PhotoMaps.Domain
{
	public class Photo
	{
		public int Id { get; set; }

		public string Title { get; set; }
		
		public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime CreateTime { get; set; }

        public UserInfo Author { get; set; }

        public PhotoCoordinate Coordinate { get; set; }

		public AccessType Access { get; set; }

		public virtual ICollection<PhotoCategory> Categories { get; set; }

        public virtual ICollection<ProtectionInformation> Protections { get; set; }

		public virtual PhotoInformation Information { get; set; }
	}
}