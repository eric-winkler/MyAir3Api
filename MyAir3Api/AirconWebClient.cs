using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    internal class AirconWebClient : IAirconWebClient
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public AirconWebClient(Uri baseAddress)
        {
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<AirconWebResponse> GetAsync(string requestUri)
        {
            var stringResponse = await _httpClient.GetStringAsync(requestUri);
            return AirconWebResponse.Parse(stringResponse);
        }
    }
}