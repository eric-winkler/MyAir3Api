namespace Winkler.MFMyAir3Api
{
    internal class AuthenticatedAirconWebClient : IAirconWebClient
    {
        private readonly IAirconWebClient _underlying;

        public AuthenticatedAirconWebClient(IAirconWebClient underlying)
        {
            _underlying = underlying;
        }

        public AirconWebResponse Get(string requestUri)
        {
            var response = _underlying.Get(requestUri);
            if (response.IsAuthenticated) return response;

            Login();
            return _underlying.Get(requestUri);
        }

        private AirconWebResponse Login()
        {
            return _underlying.Get("login?password=password");
        }
    }
}