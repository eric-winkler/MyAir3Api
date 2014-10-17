using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    public class AirconController
    {
        private readonly IAirconWebClient _aircon;

        public AirconController(IAirconWebClient aircon)
        {
            _aircon = aircon;
        }

        public async Task<AirconWebResponse> Login()
        {
            return await _aircon.GetAsync("login?password=password");
        }

        public async Task<ZoneStation> GetZoneStationAsync()
        {
            var systemData = await _aircon.GetAsync("getSystemData");
            return new ZoneStation(_aircon, systemData.InnerResponse.Element("system"));
        }
    }
}