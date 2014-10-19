namespace Winkler.MFMyAir3Api.Tests.Infrastructure
{
    public class TestAirconWebClient : IAirconWebClient
    {
        private readonly string _response;

        public string LastRequestUri { get; private set; }

        public TestAirconWebClient(string response)
        {
            _response = response;
        }

        public AirconWebResponse Get(string requestUri)
        {
            LastRequestUri = requestUri;
            return AirconWebResponse.Parse(_response);
        }
    }
}