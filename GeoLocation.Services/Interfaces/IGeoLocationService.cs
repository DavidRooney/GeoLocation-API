using System.Threading.Tasks;

namespace GeoLocation.Services.Interfaces
{
    public interface IGeoLocationService
    {
        Task<string> FetchCountriesByIPAsync(string ip, string elasticSearchEndpoint, string elasticSearchIndex, string elasticsearchCountryBlockType, string elasticsearchCountryLocationType);
    }
}
