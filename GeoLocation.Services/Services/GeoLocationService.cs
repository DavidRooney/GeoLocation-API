using GeoLocation.Services.Models;
using GeoLocation.Services.Settings;
using Nest;
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
            var node = new Uri(this.elasticSearchEndpoint);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            var countryBlock = client.Search<GeoLiteCountryBlock>(s => s.Query(q => q.Wildcard(p => p.network, "213.122.160.156")));

            return "UK";
        }
    }
}
