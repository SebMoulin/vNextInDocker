using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IGeoHash
    {
        Task<string> GetGeoLocationHash(double lat, double lng);
    }
}