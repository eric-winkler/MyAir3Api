using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    internal class AuthenticatedAirconWebClient : IAirconWebClient
    {
        private readonly IAirconWebClient _underlying;

        public AuthenticatedAirconWebClient(IAirconWebClient underlying)
        {
            _underlying = underlying;
        }

        public async Task<AirconWebResponse> GetAsync(string requestUri)
        {
            var response = await _underlying.GetAsync(requestUri).ConfigureAwait(false);
            if (response.IsAuthenticated) return response;

            await LoginAsync().ConfigureAwait(false);
            return await _underlying.GetAsync(requestUri).ConfigureAwait(false);
        }

        private async Task<AirconWebResponse> LoginAsync()
        {
            return await _underlying.GetAsync("login?password=password").ConfigureAwait(false);
        }
    }
}