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
            var response = await _underlying.GetAsync(requestUri);
            if (response.IsAuthenticated) return response;

            await new AirconController(_underlying).Login();
            return await _underlying.GetAsync(requestUri);
        }
    }
}