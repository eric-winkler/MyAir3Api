using System;
using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    public static class Aircon
    {
        public static async Task<ZoneStation> ConnectAsync()
        {
            var locater = new AirconLocater();
            var baseAddress = await locater.LocateAsync();
            if (baseAddress == null)
                throw new InvalidOperationException("Could not locate aircon");

            return await ConnectAsync(baseAddress);
        }

        public static async Task<ZoneStation> ConnectAsync(Uri baseAddress)
        {
            var aircon = BuildWebClient(baseAddress);
            return await ConnectZoneStationAsync(aircon);
        }

        internal static async Task<ZoneStation> ConnectZoneStationAsync(IAirconWebClient aircon)
        {
            var systemData = await aircon.GetAsync("getSystemData");
            return new ZoneStation(aircon, systemData.InnerResponse.Element("system"));
        }

        private static IAirconWebClient BuildWebClient(Uri baseAddress)
        {
            return new AuthenticatedAirconWebClient(new AirconWebClient(baseAddress, 5000));
        }
    }
}