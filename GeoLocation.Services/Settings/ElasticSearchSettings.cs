using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services.Settings
{
    public class ElasticSearchSettings
    {
        public string ElasticSearchEndpoint { get; set; }
        public string ElasticSearchDocumentGeoLiteCountryLocationUri { get; set; }
        public string ElasticSearchDocumentGeoLiteCountryBlockUri { get; set; }
    }
}
