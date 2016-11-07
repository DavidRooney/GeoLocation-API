using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services.Models.Elasticsearch
{
    // Country Block
    public class ElasticsearchResultsCountryBlock
    {
        [JsonProperty("hits")]
        public CountryBlockSearchHits Hits { get; set; }
    }

    public class CountryBlockSearchHits
    {
        [JsonProperty("hits")]
        public IList<ElasticsearchCountryBlockHits> Hits { get; set; }
    }

    public class ElasticsearchCountryBlockHits
    {
        [JsonProperty("_source")]
        public GeoLiteCountryBlock GeoLiteCountryBlockItem { get; set; }
    }

    // Country Location
    public class ElasticsearchResultsCountryLocation
    {
        [JsonProperty("hits")]
        public CountryLocationSearchHits Hits { get; set; }
    }

    public class CountryLocationSearchHits
    {
        [JsonProperty("hits")]
        public IList<ElasticsearchCountryLocationHits> Hits { get; set; }
    }

    public class ElasticsearchCountryLocationHits
    {
        [JsonProperty("_source")]
        public GeoLiteCountryLocation GeoLiteCountryLocationItem { get; set; }
    }
}
