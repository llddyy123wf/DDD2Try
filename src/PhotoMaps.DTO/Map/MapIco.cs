
using Newtonsoft.Json;
namespace PhotoMaps.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapIco
    {
        [JsonProperty(PropertyName="w")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "h")]
        public int Height { get; set; }

        [JsonProperty(PropertyName = "l")]
        public int ImageOffsetLeft { get; set; }

        [JsonProperty(PropertyName = "t")]
        public int ImageOffsetTop { get; set; }

        [JsonProperty(PropertyName = "lb")]
        public int AnchorBlank { get; set; }

        [JsonProperty(PropertyName = "x")]
        public int OffsetLeft { get; set; }

        [JsonProperty(PropertyName = "y")]
        public int OffsetTop { get; set; }

        [JsonProperty(PropertyName = "img")]
        public string Image { get; set; }
    }
}
