namespace Winkler.MFMyAir3Api
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
            return int.Parse(zoneString.Substring("zone".Length));
        }
    }
}