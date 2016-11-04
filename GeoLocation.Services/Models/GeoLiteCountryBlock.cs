using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services.Models
{
    public class GeoLiteCountryBlock
    {
        public string network { get; set; }
        public string network_start_ip { get; set; }
        public string network_last_ip { get; set; }
        public string network_start_integer { get; set; }
        public string network_last_integer { get; set; }
        public string geoname_id { get; set; }
        public string registered_country_geoname_id { get; set; }
        public string represented_country_geoname_id { get; set; }
        public string is_anonymous_proxy { get; set; }
        public string is_satellite_provider { get; set; }
    }
}
