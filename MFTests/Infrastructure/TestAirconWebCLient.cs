namespace Winkler.MFMyAir3Api.Tests.Infrastructure
{
    public class TestAirconWebClient : IAirconWebClient
    {
        private readonly string _response;

        public TestAirconWebClient(string response)
        {
            _response = response;
        }

        public AirconWebResponse Get(string requestUri)
        {
            return AirconWebResponse.Parse(_response);
        }
    }
}