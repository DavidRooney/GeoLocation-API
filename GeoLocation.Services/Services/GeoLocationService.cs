using GeoLocation.Services.Interfaces;
using GeoLocation.Services.Models;
using GeoLocation.Services.Models.Elasticsearch;
using Newtonsoft.Json;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeoLocation.Services.Services
{
    public interface IElasticsearchApi
    {
        [Post("_search")]
        Task<Response<string>> SearchAsync([Body] string data); 
    }

    public class GeoLocationService : IGeoLocationService
    {
        public async Task<string> FetchCountriesByIPAsync(string ip, string elasticSearchEndpoint, string elasticSearchIndex, string elasticsearchCountryBlockType, string elasticsearchCountryLocationType)
        {
            try
            {
                if (!ip.Contains("."))
                {
                    return string.Empty;
                }

                List<GeoLiteCountryBlock> resultsList = new List<GeoLiteCountryBlock>();

                var ELQueryCounrtyBlocks = new GeoLiteCountryBlockSearchQuery(ip);
                var searchJson = JsonConvert.SerializeObject(ELQueryCounrtyBlocks);
                var api = RestClient.For<IElasticsearchApi>(string.Join("/", elasticSearchEndpoint, elasticSearchIndex, elasticsearchCountryBlockType));

                var response = await api.SearchAsync(searchJson);
                if (response.ResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    return string.Empty;
                }
                else
                {
                    var results = JsonConvert.DeserializeObject<ElasticsearchResultsCountryBlock>(response.GetContent());
                    results.Hits.Hits.Select(result => result.GeoLiteCountryBlockItem).ToList().ForEach(result => resultsList.Add(result));
                }

                return string.Empty;

            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        //public string FetchCountriesByIP(string ip, string elasticSearchEndpoint, string elasticSearchIndex)
        //{
        //    try
        //    {
        //        if (!ip.Contains("."))
        //        {
        //            return string.Empty;
        //        }

        //        int intAddress = BitConverter.ToInt32(IPAddress.Parse(ip).GetAddressBytes(), 0);
        //        // string ipAddress = new IPAddress(BitConverter.GetBytes(intAddress)).ToString(); // convert int back to IP address.

        //        var node = new Uri(elasticSearchEndpoint);
        //        var settings = new ConnectionSettings(node);
        //        var client = new ElasticClient(settings);
        //        var indexSettings = new IndexSettings();
        //        indexSettings.NumberOfReplicas = 1;
        //        indexSettings.NumberOfShards = 1;

        //        var countryBlock = client.Search<GeoLiteCountryBlock>(s => s
        //                                .Query(q => q
        //                                    .Bool(b => b
        //                                        .Filter(f => f
        //                                            .Bool(b1 => b1
        //                                                .Must(m => m
        //                                                    .Bool(b2 => b2
        //                                                        .Must(m1 => m1
        //                                                            .Range(r => r.Field("network_start_ip").GreaterThanOrEquals(Convert.ToDouble(ip)))
        //                                                        ).Must(m1 => m1
        //                                                            .Range(r => r.Field("network_last_ip").LessThanOrEquals(Convert.ToDouble(ip))))))))))
        //                                .Index(elasticSearchIndex));

        //        var LocationIDs = countryBlock.Hits.Select(h => h.Source.geoname_id);
        //        List<string> possibleCountries = new List<string>();

        //        foreach (var id in LocationIDs)
        //        {
        //            var countryLocation = client.Search<GeoLiteCountryLocation>(s => s
        //                                .Query(q => q
        //                                    .Bool(b => b
        //                                        .Filter(f => f
        //                                            .Bool(b1 => b1
        //                                                .Must(m => m
        //                                                    .Bool(b2 => b2
        //                                                        .Should(m1 => m1
        //                                                            .Match(ma => ma.Field("geoname_id").Query(id)))))))))
        //                                .Index(elasticSearchIndex));

        //            possibleCountries.Add(countryLocation.Hits.Select(h => h.Source.country_name).FirstOrDefault());
        //        }

        //        return possibleCountries.FirstOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        return string.Empty;
        //    }
        //}
    }
}
