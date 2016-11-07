using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services.Models
{
    public class GeoLiteCountryBlock
    {
        [JsonProperty("network")]
        public string network { get; set; }

        [JsonProperty("network_start_ip")]
        public string network_start_ip { get; set; }

        [JsonProperty("network_last_ip")]
        public string network_last_ip { get; set; }

        [JsonProperty("network_start_integer")]
        public string network_start_integer { get; set; }

        [JsonProperty("network_last_integer")]
        public string network_last_integer { get; set; }

        [JsonProperty("geoname_id")]
        public string geoname_id { get; set; }

        [JsonProperty("registered_country_geoname_id")]
        public string registered_country_geoname_id { get; set; }

        [JsonProperty("represented_country_geoname_id")]
        public string represented_country_geoname_id { get; set; }

        [JsonProperty("is_anonymous_proxy")]
        public string is_anonymous_proxy { get; set; }

        [JsonProperty("is_satellite_provider")]
        public string is_satellite_provider { get; set; }
    }
}
