using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services.Models.Elasticsearch
{
    public class ElasticsearchResultsCountryBlock
    {
        [JsonProperty("hits")]
        public SearchHits Hits { get; set; }
    }

    public class SearchHits
    {
        [JsonProperty("hits")]
        public IList<ElasticsearchCountryBlockHits> Hits { get; set; }
    }

    public class ElasticsearchCountryBlockHits
    {
        [JsonProperty("_source")]
        public GeoLiteCountryBlock GeoLiteCountryBlockItem { get; set; }
    }
}
