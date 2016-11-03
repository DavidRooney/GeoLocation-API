using GeoLocation.Importer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace GeoLocation.Importer.Processors
{
    public class GeoLite2CountryBlockProcessor : IBaseProcessor
    {
        // NOTE: The latest files can be found here: http://dev.maxmind.com/geoip/geoip2/geolite2/

        public GeoLite2CountryBlockProcessor(IEnumerable<GeoLite2CountryBlockDefinition> modelList)
        {
            this.ModelList = modelList;
            this.elasticSearchEndpoint = ConfigurationManager.AppSettings["ElasticSearchEndpoint"];
            this.elasticSearchDocumentGeoLiteCountryBlockUri = ConfigurationManager.AppSettings["ElasticSearchDocumentGeoLiteCountryBlockUri"];
        }

        private IEnumerable<GeoLite2CountryBlockDefinition> ModelList { get; set; }

        private readonly string elasticSearchEndpoint;

        private readonly string elasticSearchDocumentGeoLiteCountryBlockUri;

        public bool Import()
        {
            try
            {
                // Insert into elasticsearch database
                int inserted = 0;
                var client = new RestClient(this.elasticSearchEndpoint);
                foreach (var item in this.ModelList)
                {
                    var request = new RestRequest(this.elasticSearchDocumentGeoLiteCountryBlockUri, Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(item);
                    var response = client.Execute(request);
                    if (!string.IsNullOrEmpty(response.ErrorMessage))
                    {
                        return false;
                    }
                    else
                    {
                        inserted++;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
