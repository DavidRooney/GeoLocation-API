using GeoLocation.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services.Services
{
    public class GeoLocationService
    {
        public GeoLocationService(ElasticSearchSettings settings)
        {
            this.elasticSearchEndpoint = settings.ElasticSearchEndpoint;
            this.elasticSearchDocumentGeoLiteCountryBlockUri = settings.ElasticSearchDocumentGeoLiteCountryBlockUri;
            this.elasticSearchDocumentGeoLiteCountryLocationUri = settings.ElasticSearchDocumentGeoLiteCountryLocationUri;
        }

        private readonly string elasticSearchEndpoint;

        private readonly string elasticSearchDocumentGeoLiteCountryBlockUri;

        private readonly string elasticSearchDocumentGeoLiteCountryLocationUri;

        public string FetchCountryByIP(string ip)
        {

            return "UK";
        }
    }
}
