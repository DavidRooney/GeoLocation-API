using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services.Models
{
    public class GeoLiteCountryLocation
    {
        public string geoname_id { get; set; }
        public string locale_code { get; set; }
        public string continent_code { get; set; }
        public string continent_name { get; set; }
        public string country_iso_code { get; set; }
        public string country_name { get; set; }
    }
}
