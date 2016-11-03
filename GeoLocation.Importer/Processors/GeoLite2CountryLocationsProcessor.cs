using GeoLocation.Importer.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocation.Importer.Processors
{
    public class GeoLite2CountryLocationsProcessor : IBaseProcessor
    {
        // NOTE: The latest files can be found here: http://dev.maxmind.com/geoip/geoip2/geolite2/

        public GeoLite2CountryLocationsProcessor(IEnumerable<GeoLite2CountryLocationsDefinition> modelList)
        {
            this.ModelList = modelList;
        }

        private IEnumerable<GeoLite2CountryLocationsDefinition> ModelList { get; set; }

        public bool Import()
        {
            try
            {
                // Insert into elasticsearch database
                int inserted = 0;
                var client = new RestClient("http://localhost:9200");
                foreach (var item in this.ModelList)
                {
                    var request = new RestRequest("tla-elasticsearch-geo-location/geolitecountrylocation", Method.POST);
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
