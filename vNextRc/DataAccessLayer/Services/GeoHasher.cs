using System;
using System.Threading.Tasks;
using DataAccessLayer.Contracts;
using Framework.Configuration.Contracts;
using Framework.Http.Contracts;

namespace DataAccessLayer.Services
{
    public class GeoHasher : IGeoHash
    {
        private readonly IHandleHttpRequest _httpRequestHandler;

        public GeoHasher(IProvideConfig configProvider, IHandleHttpRequest httpRequestHandler)
        {
            if (configProvider == null) throw new ArgumentNullException("configProvider");
            if (httpRequestHandler == null) throw new ArgumentNullException("httpRequestHandler");
            _httpRequestHandler = httpRequestHandler;
            _httpRequestHandler.Init(configProvider.GetGeoHashBaseUrl(), string.Empty);
        }

        public async Task<string> GetGeoLocationHash(double lat, double lng)
        {
            var result = await _httpRequestHandler.GetHttpRequest(string.Format("?q={0},{1}&format=url&redirect=0", lat, lng));
            var test = result.Replace("http://geohash.org/", string.Empty);
            //var indexOfSlash = result.LastIndexOf('/') + 1;
            //var hash = result.Substring(indexOfSlash, result.Length);
            return test;
        }
    }
}