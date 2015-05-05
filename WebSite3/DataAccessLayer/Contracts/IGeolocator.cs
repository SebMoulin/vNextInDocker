using System.Threading.Tasks;
using Commons.Entities.Geolocation;
using Framework.Http;

namespace DataAccessLayer.Contracts
{
    public interface IGeolocator
    {
        Task<GeoLocation> GetGeoCoordonateByCity(string city);
    }
}
