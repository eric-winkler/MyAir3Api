using System;

namespace Winkler.MFMyAir3Api
{
    public static class Aircon
    {
        public static ZoneStation Connect()
        {
            var locater = new AirconLocater();
            var baseAddress = locater.Locate();
            if (baseAddress == null)
                throw new InvalidOperationException("Could not locate aircon");

            return Connect(baseAddress);
        }

        public static ZoneStation Connect(Uri baseAddress)
        {
            var aircon = BuildWebClient(baseAddress);
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