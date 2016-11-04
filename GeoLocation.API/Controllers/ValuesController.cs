using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using GeoLocation.Services.Interfaces;

namespace GeoLocation.API.Controllers
{
    [Route("api/")]
    public class ValuesController : ApiController
    {
        private IGeoLocationService geoLocationService;

        [Route("")]
        public IActionResult GetCountryByIp([FromUri]string ip)
        {
            var result = this.geoLocationService.FetchCountriesByIP(ip);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
