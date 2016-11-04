using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Services
{
    public interface IGeoLocationService
    {
        string FetchCountriesByIP(string ip);
    }
}
