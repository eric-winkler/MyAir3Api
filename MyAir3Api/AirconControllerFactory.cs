using System.Web.Compilation;

namespace Winkler.MyAir3Api
{
    public class AirconControllerFactory
    {
        public AirconController Build(string baseAddress)
        {
            return new AirconController(new AuthenticatedAirconWebClient(new AirconWebClient(baseAddress)));
        }
    }
}