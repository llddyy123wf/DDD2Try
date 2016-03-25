using PhotoMaps.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoMaps.ViewModel
{
    /// <summary>
    ///  Photo View(保存用)
    /// </summary>
    public class PhotoInfo
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public Photo Parse()
        {
            Photo target = new Photo()
            {
                Coordinate = new PhotoCoordinate() { Latitude = this.Latitude, Longitude = this.Longitude },
                CreateTime = DateTime.Now,
                Description = this.Description,
                PhotoUrl = this.Url,
                Title = this.Title,
                Access = AccessType.Public,
                Author = new UserInfo() { Id = 1,UserName="Jimmy" },
            };
            return target;
        }
    }
}
