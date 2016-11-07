using Newtonsoft.Json;

namespace GeoLocation.Services.Models
{
    public class GeoLiteCountryLocation
    {
        [JsonProperty("geoname_id")]
        public string geoname_id { get; set; }

        [JsonProperty("locale_code")]
        public string locale_code { get; set; }

        [JsonProperty("continent_code")]
        public string continent_code { get; set; }

        [JsonProperty("continent_name")]
        public string continent_name { get; set; }

        [JsonProperty("country_iso_code")]
        public string country_iso_code { get; set; }

        [JsonProperty("country_name")]
        public string country_name { get; set; }
    }
}
