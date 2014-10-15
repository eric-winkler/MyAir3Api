using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class Zone
    {
        private readonly IAirconWebClient _aircon;

        public int Number { get; private set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int UserPercentSetting { get; set; }
        public int MaxDamper { get; private set; }
        public int MinDamper { get; private set; }
        public bool UserPercentAvailable { get; private set; }
        public decimal ActualTemp { get; private set; }
        public decimal DesiredTemp { get; private set; }
        public decimal RfStrength { get; private set; }
        public bool HasLowBattery { get; private set; }
        public bool HasMotorError { get; private set; }
        public bool HasClimateControl { get; private set; }

        public Zone(IAirconWebClient aircon, int number, XElement zoneData)
        {
            _aircon = aircon;
            zoneData = zoneData.Element("zone" + number);

            Number = number;
            Name = zoneData.Element("name").Value;
            Enabled = int.Parse(zoneData.Element("setting").Value) == 1;
            UserPercentSetting = int.Parse(zoneData.Element("userPercentSetting").Value);
            MaxDamper = int.Parse(zoneData.Element("maxDamper").Value);
            MinDamper = int.Parse(zoneData.Element("minDamper").Value);
            UserPercentAvailable = int.Parse(zoneData.Element("userPercentAvail").Value) == 1;
            ActualTemp = decimal.Parse(zoneData.Element("actualTemp").Value);
            DesiredTemp = decimal.Parse(zoneData.Element("desiredTemp").Value);
            RfStrength = decimal.Parse(zoneData.Element("RFstrength").Value);
            HasLowBattery = int.Parse(zoneData.Element("hasLowBatt").Value) == 1;
            HasMotorError = int.Parse(zoneData.Element("hasMotorError").Value) == 1;
            HasClimateControl = int.Parse(zoneData.Element("hasClimateControl").Value) == 1;
        }

        public async Task<AirconWebResponse> UpdateAsync()
        {
            return await _aircon.GetAsync("setZoneData?"
                + "zone=" + Number
                + "&zoneSetting=" + (Enabled ? "1" : "0")
                + "&name=" + HttpUtility.UrlEncode(Name)
                + "&userPercentSetting=" + UserPercentSetting);
        }
    }
}