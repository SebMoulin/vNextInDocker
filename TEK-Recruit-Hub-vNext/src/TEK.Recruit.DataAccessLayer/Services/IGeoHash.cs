using System.Threading.Tasks;

namespace TEK.Recruit.DataAccessLayer.Services
{
    public interface IGeoHash
    {
        Task<string> GetGeoLocationHash(double lat, double lng);
    }
}