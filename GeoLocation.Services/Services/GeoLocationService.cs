using GeoLocation.Services.Models;
using GeoLocation.Services.Settings;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeoLocation.Services.Services
{
    public class GeoLocationService : IGeoLocationService
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

        public string FetchCountriesByIP(string ip)
        {
            int intAddress = BitConverter.ToInt32(IPAddress.Parse(ip).GetAddressBytes(), 0);
            // string ipAddress = new IPAddress(BitConverter.GetBytes(intAddress)).ToString(); // convert int back to IP address.

            var node = new Uri(this.elasticSearchEndpoint);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            var countryBlock = client.Search<GeoLiteCountryBlock>(s => s
                                    .Query(q => q
                                        .Bool(b => b
                                            .Filter(f => f
                                                .Bool(b1 => b1
                                                    .Must(m => m
                                                        .Bool(b2 => b2
                                                            .Must(m1 => m1
                                                                .Range(r => r.Field("network_start_integer").GreaterThan(intAddress))
                                                            ).Must(m1 => m1
                                                                .Range(r => r.Field("network_end_integer").LessThan(intAddress))))))))));

            var LocationIDs = countryBlock.Hits.Select(h => h.Source.geoname_id);
            List<string> possibleCountries = new List<string>();

            foreach (var id in LocationIDs)
            {
                var countryLocation = client.Search<GeoLiteCountryLocation>(s => s
                                    .Query(q => q
                                        .Bool(b => b
                                            .Filter(f => f
                                                .Bool(b1 => b1
                                                    .Must(m => m
                                                        .Bool(b2 => b2
                                                            .Should(m1 => m1
                                                                .Match(ma => ma.Field("geoname_id").Query(id))
                                                            ))))))));

                possibleCountries.Add(countryLocation.Hits.Select(h => h.Source.country_name).FirstOrDefault());
            }

            return possibleCountries.FirstOrDefault();
        }
    }
}
