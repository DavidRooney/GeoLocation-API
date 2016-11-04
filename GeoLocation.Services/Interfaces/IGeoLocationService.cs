namespace GeoLocation.Services.Interfaces
{
    public interface IGeoLocationService
    {
        string FetchCountriesByIP(string ip);
    }
}
