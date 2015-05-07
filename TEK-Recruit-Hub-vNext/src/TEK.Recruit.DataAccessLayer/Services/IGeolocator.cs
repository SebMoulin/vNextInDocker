using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Geolocation;

namespace TEK.Recruit.DataAccessLayer.Services
{
    public interface IGeolocator
    {
        Task<GeoLocation> GetGeoCoordonateByCity(string city);
    }
}
