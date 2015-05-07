using System;
using System.Threading.Tasks;
using TEK.Recruit.Commons.Entities.Geolocation;
using TEK.Recruit.Framework.Configuration.Services;
using TEK.Recruit.Framework.Http.Services;

namespace TEK.Recruit.DataAccessLayer.Services
{
    public class Geolocator : IGeolocator
    {
        private readonly IHandleHttpRequest _httpRequestHandler;

        public Geolocator(IProvideConfig configProvider, IHandleHttpRequest httpRequestHandler)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (httpRequestHandler == null) throw new ArgumentNullException("httpRequestHandler");
            _httpRequestHandler = httpRequestHandler;
            _httpRequestHandler.Init(configProvider.GetGeolocatorBaseUrl(), string.Empty);
        }

        public async Task<GeoLocation> GetGeoCoordonateByCity(string city)
        {
            return await _httpRequestHandler.GetHttpRequest<GeoLocation>(string.Format("fulltext/fulltextsearch?q={0}&placetype=city&from=1&to=1&indent=true&format=json", city), null);
        }
    }
}