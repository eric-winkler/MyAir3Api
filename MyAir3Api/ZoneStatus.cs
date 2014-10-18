using System;
using System.Linq;
using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class ZoneStatus
    {
        public int ZoneNumber { get; private set; }
        public bool Enabled { get; set; }
        
        public ZoneStatus(XElement element)
        {
            ZoneNumber = ExtractZoneNumber(element.Name.ToString());
            Enabled = int.Parse(element.Value) == 1;
        }

        private static int ExtractZoneNumber(string zoneString)
        {
            return int.Parse(zoneString.Split(new[] { "zone" }, StringSplitOptions.RemoveEmptyEntries).Single());
        }
    }
}