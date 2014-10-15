using System.Web.Compilation;

namespace Winkler.MyAir3Api
{
    public static class AirconControllerFactory
    {
        public static AirconController Build(string baseAddress)
        {
            return new AirconController(new AuthenticatedAirconWebClient(new AirconWebClient(baseAddress)));
        }
    }
}