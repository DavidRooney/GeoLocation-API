using GeoLocation.Services.Services;
using GeoLocation.Tests.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xunit;

namespace GeoLocation.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class IPIntegrationTests
    {
        // NOTE: if you are getting an error similar to the following then use the link below to update your version of VS Tooling to Preview2.
        // Error: dotnet-test-xunit The dependency Microsoft.Extensions.Testing.Abstraction does not support framework .NETCoreApp
        // To test your dotnet version goto a command prompt and run "dotnet --version". this should be something like "1.0.0-preview2-003131"
        // Direct link: https://go.microsoft.com/fwlink/?LinkId=827546
        // website link: https://www.microsoft.com/net/download

        private readonly string ElasticSearchEndpoint = "http://localhost:9200";
        private readonly string ElasticSearchIndex = "tla-elasticsearch-geo-location";
        private readonly string ElasticSearchTypeGeoLiteCountryBlockUri = "geolitecountrylocation";
        private readonly string ElasticSearchTypeGeoLiteCountryLocationUri = "geolitecountryblock";

        [Fact]
        public void GetCountryByIpTest()
        {
            // TODO: Currently running the service like this does not access the database. Needs investigating. could be something to do with the method be Async

            GeoLocationService service = new GeoLocationService();

            var file = File.ReadAllText("Resources\\TestIPAddresses.xml");
            var doc = XDocument.Parse(file);

            var testIPs = (from r in doc.Root.Elements()
                         select new Rule()
                         {
                             country = (string)r.Element("country"),
                             ip = (string)r.Element("ip")
                         });

            foreach (var IP in testIPs)
            {
                var countries = service.FetchCountriesByIPAsync(
                                    IP.ip,
                                    this.ElasticSearchEndpoint,
                                    this.ElasticSearchIndex,
                                    this.ElasticSearchTypeGeoLiteCountryBlockUri,
                                    this.ElasticSearchTypeGeoLiteCountryLocationUri)
                            .Result;

                //Assert.Contains(IP.country, countries);
            }
        }
    }
}
