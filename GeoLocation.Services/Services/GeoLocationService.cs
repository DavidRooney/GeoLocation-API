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

                // Fetch Country Block data from elasticsearch
                List<GeoLiteCountryBlock> resultsList = new List<GeoLiteCountryBlock>();
                var ELQueryCounrtyBlocks = new GeoLiteCountryBlockSearchQuery(ip);
                var countryBlockSearchJson = JsonConvert.SerializeObject(ELQueryCounrtyBlocks);
                var apiCountryBlock = RestClient.For<IElasticsearchApi>(string.Join("/", elasticSearchEndpoint, elasticSearchIndex, elasticsearchCountryBlockType));

                var countryBlockResponse = await apiCountryBlock.SearchAsync(countryBlockSearchJson);
                if (countryBlockResponse.ResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    return string.Empty;
                }
                else
                {
                    var results = JsonConvert.DeserializeObject<ElasticsearchResultsCountryBlock>(countryBlockResponse.StringContent);
                    results.Hits.Hits.Select(result => result.GeoLiteCountryBlockItem).ToList().ForEach(result => resultsList.Add(result));
                }

                // Fetch Country Location data from elasticsearch
                List<GeoLiteCountryLocation> locationList = new List<GeoLiteCountryLocation>();
                var ELQueryCounrtyLocation = new GeoLiteCountryLocationSearchQuery(resultsList.Select(r => r.geoname_id));
                var countryLocationSearchJson = JsonConvert.SerializeObject(ELQueryCounrtyLocation);
                var apiCountryLocation = RestClient.For<IElasticsearchApi>(string.Join("/", elasticSearchEndpoint, elasticSearchIndex, elasticsearchCountryLocationType));

                var countryLocationResponse = await apiCountryLocation.SearchAsync(countryLocationSearchJson);
                if (countryLocationResponse.ResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    return string.Empty;
                }
                else
                {
                    var results = JsonConvert.DeserializeObject<ElasticsearchResultsCountryLocation>(countryLocationResponse.StringContent);
                    results.Hits.Hits.Select(result => result.GeoLiteCountryLocationItem).ToList().ForEach(result => locationList.Add(result));
                }

                return string.Join("|", locationList.Select(r => r.country_name));

            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}
