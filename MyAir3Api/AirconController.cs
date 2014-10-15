﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    public class AirconController
    {
        private const int NumberOfZones = 10;
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

        public async Task<IEnumerable<Zone>> GetZonesAsync()
        {
            var zoneRetrievalTasks = Enumerable.Range(1, NumberOfZones)
                .Select(z => new
                {
                    ZoneId = z,
                    ZoneTask = _aircon.GetAsync("getZoneData?zone=" + z)
                }).ToArray();
            await Task.WhenAll(zoneRetrievalTasks.Select(z => z.ZoneTask));

            return zoneRetrievalTasks.Select(z => new Zone(_aircon, z.ZoneId, z.ZoneTask.Result.InnerResponse));
        }
    }
}