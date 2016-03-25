
using Newtonsoft.Json;
namespace PhotoMaps.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapPoint
    {
        [JsonProperty(PropertyName = "lng")]
        public string Longitude { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public string Latitude { get; set; }
    }
}
