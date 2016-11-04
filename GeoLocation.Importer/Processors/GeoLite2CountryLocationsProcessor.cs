using GeoLocation.Importer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace GeoLocation.Importer.Processors
{
    public class GeoLite2CountryLocationsProcessor : IBaseProcessor
    {
        // NOTE: The latest files can be found here: http://dev.maxmind.com/geoip/geoip2/geolite2/
        // Documentation for the files can be found here: http://dev.maxmind.com/geoip/geoip2/geoip2-city-country-csv-databases/
        // Original downloaded files DO NOT contain the IP range. These need to be generated using a utility which can be downloaded from here: https://github.com/maxmind/geoip2-csv-converter/releases
        // How to use the utility: 
        // 1. Move the .exe file into the same directory as the .csv files. 
        // 2. shift click and open a CMD prompt.
        // 3. Run the following to produce the output needed to import into elasticsearch: geoip2-csv-converter.exe -block-file=GeoLite2-Country-Blocks-IPv4.csv -output-file=GeoLite2-Country-Blocks-IPv4-formatted.csv -include-cidr -include-range -include-integer-range
        // delete the old file and rename the new formatted file to "GeoLite2-Country-Blocks-IPv4.csv".

        public GeoLite2CountryLocationsProcessor()
        {
            this.elasticSearchEndpoint = ConfigurationManager.AppSettings["ElasticSearchEndpoint"];
            this.elasticSearchDocumentGeoLiteCountryLocationUri = ConfigurationManager.AppSettings["ElasticSearchDocumentGeoLiteCountryLocationUri"];
            this.elasticSearchDocumentGeoLiteIndexUri = ConfigurationManager.AppSettings["ElasticSearchDocumentGeoLiteIndexUri"];
        }

        public GeoLite2CountryLocationsProcessor(IEnumerable<GeoLite2CountryLocationsDefinition> modelList) : this()
        {
            this.ModelList = modelList;
        }

        private IEnumerable<GeoLite2CountryLocationsDefinition> ModelList { get; set; }
        private readonly string elasticSearchEndpoint;
        private readonly string elasticSearchDocumentGeoLiteCountryLocationUri;
        private readonly string elasticSearchDocumentGeoLiteIndexUri;

        public bool DeleteIndex(out string message)
        {
            message = string.Empty;

            try
            {
                var client = new RestClient(this.elasticSearchEndpoint);
                var request = new RestRequest(this.elasticSearchDocumentGeoLiteIndexUri, Method.DELETE);

                var response = client.Execute(request);

                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    message = string.Format("An error occurred: {0}", response.ErrorMessage);
                    return false;
                }

                message = "Elasticsearch index deleted successfully!";

                return true;
            }
            catch (Exception e)
            {
                message = string.Format("An error occurred: {0}", e.Message);
                return false;
            }
        }

        public bool Import()
        {
            try
            {
                // Insert into elasticsearch database
                int inserted = 0;
                var client = new RestClient(this.elasticSearchEndpoint);
                foreach (var item in this.ModelList)
                {
                    var request = new RestRequest(this.elasticSearchDocumentGeoLiteCountryLocationUri, Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(item);

                    //TODO: Add functionality to stop duplicates from being added to elasticsearch type

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
