using System;

namespace Winkler.MFMyAir3Api
{
    public static class Aircon
    {
        public static ZoneStation Connect(string baseAddress)
        {
            var aircon = BuildWebClient(new Uri(baseAddress));
            return ConnectZoneStation(aircon);
        }

        public static ZoneStation ConnectZoneStation(IAirconWebClient aircon)
        {
            var systemData = aircon.Get("getSystemData");
            return new ZoneStation(aircon, systemData.InnerResponse.Element("system"));
        }

        private static IAirconWebClient BuildWebClient(Uri baseAddress)
        {
            return new AuthenticatedAirconWebClient(new AirconWebClient(baseAddress, 5000));
        }
    }
}