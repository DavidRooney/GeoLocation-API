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
                            MustList = new List<Must>()
                            {
                                new Must { Range = new Range { network_start_ip = new LessThanOrEquals { To = ip } } },
                                new Must { Range = new Range { network_last_ip = new GreaterThanOrEquals { From = ip } } }
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

    public class GeoLiteCountryLocationSearchQuery
    {
        public GeoLiteCountryLocationSearchQuery(IEnumerable<string> geonameIds)
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
                            MustList = new List<Must>()
                            {
                                new Must
                                {
                                    Bool = new Bool
                                    {
                                        Should = new List<Should>()
                                        {
                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            foreach (var id in geonameIds)
            {
                this.Query.Bool.Filter.Bool.MustList.FirstOrDefault().Bool.Should.Add(new Should { Match = new Match { geoname_id = new MatchField { Query = id, Type = "phrase" } } });
            }
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
        public IList<Must> MustList { get; set; }

        [JsonProperty("filter", NullValueHandling = NullValueHandling.Ignore)]
        public Filter Filter { get; set; }

        [JsonProperty("should", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Should> Should { get; set; }
    }

    public class Filter
    {
        [JsonProperty("bool", NullValueHandling = NullValueHandling.Ignore)]
        public Bool Bool { get; set; }
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
        public LessThanOrEquals network_start_ip { get; set; }

        [JsonProperty("network_last_ip", NullValueHandling = NullValueHandling.Ignore)]
        public GreaterThanOrEquals network_last_ip { get; set; }
    }

    public class GreaterThanOrEquals
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

    public class LessThanOrEquals
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
