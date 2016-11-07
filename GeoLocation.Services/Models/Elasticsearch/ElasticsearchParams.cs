using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services.Models.Elasticsearch
{
    public class GeoLiteCountryBlockSearchQuery
    {
        public GeoLiteCountryBlockSearchQuery(string ip)
        {
            this.Size = 200;
            this.From = 0;
            this.Query = new Query
            {
                Bool = new Bool
                {
                    Filter = new Filter
                    {
                        Bool = new Bool
                        {
                            Must = new Must
                            {
                                Bool = new Bool
                                {
                                    MustList = new MustList
                                    {
                                        List = new List<Must>()
                                        {
                                            new Must { Range = new Range { network_start_ip = new GreaterThanOrEquals { To = ip } } },
                                            new Must { Range = new Range { network_last_ip = new LessThanOrEquals { From = ip } } }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        [JsonProperty("from")]
        public int From { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }
    }

    public class Query
    {
        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public Bool Bool { get; set; }
    }

    public class Bool
    {
        [JsonProperty("must", NullValueHandling = NullValueHandling.Ignore)]
        public Must Must { get; set; }

        public MustList MustList { get; set; }

        [JsonProperty("filter", NullValueHandling = NullValueHandling.Ignore)]
        public Filter Filter { get; set; }

        [JsonProperty("should", NullValueHandling = NullValueHandling.Ignore)]
        public Should Should { get; set; }
    }

    public class Filter
    {
        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public Bool Bool { get; set; }
    }

    public class MustList
    {
        [JsonProperty("must", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Must> List { get; set; }
    }

    public class Must
    {
        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public Range Range { get; set; }

        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public Bool Bool { get; set; }
    }

    public class Range
    {
        [JsonProperty("network_start_ip", NullValueHandling = NullValueHandling.Ignore)]
        public GreaterThanOrEquals network_start_ip { get; set; }

        [JsonProperty("network_last_ip", NullValueHandling = NullValueHandling.Ignore)]
        public LessThanOrEquals network_last_ip { get; set; }
    }

    public class GreaterThanOrEquals
    {
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public string To { get; set; }

        [JsonProperty("from")]
        public string From { get { return null; } }

        [JsonProperty("include_lower")]
        public bool Include_lower { get { return true; } }

        [JsonProperty("include_upper")]
        public bool Include_upper { get { return true; } }
    }

    public class LessThanOrEquals
    {
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get { return null; } }

        [JsonProperty("include_lower")]
        public bool Include_lower { get { return true; } }

        [JsonProperty("include_upper")]
        public bool Include_upper { get { return true; } }
    }

    public class Should
    {
        [JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
        public Match Match { get; set; }
    }

    public class Match
    {
        [JsonProperty("geoname_id", NullValueHandling = NullValueHandling.Ignore)]
        public MatchField geoname_id { get; set; }
    }

    public class MatchField
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
