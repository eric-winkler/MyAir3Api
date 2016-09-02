using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    internal class AirconWebClient : IAirconWebClient
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public AirconWebClient(Uri baseAddress, int timeout)
        {
            _httpClient.BaseAddress = baseAddress;
            _httpClient.Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public async Task<AirconWebResponse> GetAsync(string requestUri)
        {
            var stringResponse = await _httpClient.GetStringAsync(requestUri).ConfigureAwait(false);
            return AirconWebResponse.Parse(stringResponse);
        }
    }
}