using Newtonsoft.Json;
using PhotoMaps.Domain;

namespace PhotoMaps.ViewModel
{
    // Photo(显示用)
    [JsonObject(MemberSerialization.OptIn)]
    public class PhotoView
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "img")]
        public string PhotoUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [JsonProperty(PropertyName = "icon")]
        public MapIco Ico { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        [JsonProperty(PropertyName = "point")]
        public MapPoint Point { get; set; }

        public void Parse(Photo photo)
        {
            this.Title = photo.Title;
            this.Content = photo.Description;
            this.PhotoUrl = photo.PhotoUrl;
            this.Ico = new MapIco()
            {
                Width = 21,
                Height = 21,
                ImageOffsetLeft = 115,
                ImageOffsetTop = 46,
                OffsetLeft = 1,
                OffsetTop = 1,
                AnchorBlank = 10
            };

            this.Point = new MapPoint()
            {
                Longitude = photo.Coordinate.Longitude.ToString(),
                Latitude = photo.Coordinate.Latitude.ToString(),
            };
        }
    }
}
