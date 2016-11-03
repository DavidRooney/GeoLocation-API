using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocation.Importer.Models
{
    public class GeoLite2CountryLocationsDefinitionMap : CsvClassMap<GeoLite2CountryLocationsDefinition>
    {
        public GeoLite2CountryLocationsDefinitionMap()
        {
            Map(m => m.geoname_id).Name("geoname_id");
            Map(m => m.locale_code).Name("locale_code");
            Map(m => m.continent_code).Name("continent_code");
            Map(m => m.continent_name).Name("continent_name");
            Map(m => m.country_iso_code).Name("country_iso_code");
            Map(m => m.country_name).Name("country_name");
        }
    }

    public class GeoLite2CountryLocationsDefinition
    {
        public string geoname_id { get; set; }
        public string locale_code { get; set; }
        public string continent_code { get; set; }
        public string continent_name { get; set; }
        public string country_iso_code { get; set; }
        public string country_name { get; set; }
    }

    public class GeoLite2CountryBlockDefinitionMap : CsvClassMap<GeoLite2CountryBlockDefinition>
    {
        public GeoLite2CountryBlockDefinitionMap()
        {
            Map(m => m.network).Name("network");
            Map(m => m.geoname_id).Name("geoname_id");
            Map(m => m.registered_country_geoname_id).Name("registered_country_geoname_id");
            Map(m => m.represented_country_geoname_id).Name("represented_country_geoname_id");
            Map(m => m.is_anonymous_proxy).Name("is_anonymous_proxy");
            Map(m => m.is_satellite_provider).Name("is_satellite_provider");
        }
    }

    public class GeoLite2CountryBlockDefinition
    {
        public string network { get; set; }
        public string geoname_id { get; set; }
        public string registered_country_geoname_id { get; set; }
        public string represented_country_geoname_id { get; set; }
        public string is_anonymous_proxy { get; set; }
        public string is_satellite_provider { get; set; }
    }
}
