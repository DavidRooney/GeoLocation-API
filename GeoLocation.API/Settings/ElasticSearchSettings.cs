using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.API.Settings
{
    public class ElasticSearchSettings
    {
        public string ElasticSearchEndpoint { get; set; }
        public string ElasticSearchIndex { get; set; }
        public string ElasticSearchTypeGeoLiteCountryLocationUri { get; set; }
        public string ElasticSearchTypeGeoLiteCountryBlockUri { get; set; }
    }
}
