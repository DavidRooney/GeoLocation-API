using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using GeoLocation.Services.Interfaces;
using GeoLocation.API.Settings;
using Microsoft.Extensions.Options;

namespace GeoLocation.API.Controllers
{
    [Route("api/")]
    public class GeoLocationAPIController : Controller
    {
        private readonly IGeoLocationService geoLocationService;
        private readonly ElasticSearchSettings elasticSearchSettings;

        public GeoLocationAPIController(IGeoLocationService geoLocationService, IOptions<ElasticSearchSettings> elasticSearchSettings)
        {
            this.geoLocationService = geoLocationService;
            this.elasticSearchSettings = elasticSearchSettings.Value;
        }

        [HttpGet("{ip}")]
        [Produces(typeof(string))]
        public async Task<IActionResult> GetCountryByIp([FromRoute]string ip)
        {
            var result = await this.geoLocationService.FetchCountriesByIPAsync(ip, elasticSearchSettings.ElasticSearchEndpoint, elasticSearchSettings.ElasticSearchIndex, elasticSearchSettings.ElasticSearchTypeGeoLiteCountryBlockUri, elasticSearchSettings.ElasticSearchTypeGeoLiteCountryLocationUri);

            if (!string.IsNullOrEmpty(result))
            {
                return new OkObjectResult(result);
            }

            return this.NoContent();
        }
    }
}
