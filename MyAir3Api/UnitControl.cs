using System.Threading.Tasks;
using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class UnitControl
    {
        private readonly IAirconWebClient _aircon;

        public bool Power { get; private set; }
        public FanSpeed FanSpeed { get; set; }
        public InverterMode InverterMode { get; set; }
        public bool TempsSetting { get; private set; }
        public decimal CentralActualTemp { get; private set; }
        public decimal CentralDesiredTemp { get; set; }
        public string ErrorCode { get; private set; }
        public string ActivationCodeStatus { get; private set; }
        public int NumberOfZones { get; private set; }
        public decimal MaxUserTemp { get; private set; }
        public decimal MinUserTemp { get; private set; }
        public int NumberOfSchedules { get; private set; }
        
        public UnitControl(IAirconWebClient aircon, XElement data)
        {
            _aircon = aircon;

            Power = int.Parse(data.Element("airconOnOff").Value) == 1;
            FanSpeed = (FanSpeed) int.Parse(data.Element("fanSpeed").Value);
            InverterMode = (InverterMode) int.Parse(data.Element("mode").Value);
            TempsSetting = int.Parse(data.Element("unitControlTempsSetting").Value) == 1;
            CentralActualTemp = decimal.Parse(data.Element("centralActualTemp").Value);
            CentralDesiredTemp = decimal.Parse(data.Element("centralDesiredTemp").Value);
            ErrorCode = data.Element("airConErrorCode").Value;
            ActivationCodeStatus = data.Element("activationCodeStatus").Value;
            NumberOfZones = int.Parse(data.Element("numberOfZones").Value);
            MaxUserTemp = decimal.Parse(data.Element("maxUserTemp").Value);
            MinUserTemp = decimal.Parse(data.Element("minUserTemp").Value);
            NumberOfSchedules = int.Parse(data.Element("availableSchedules").Value);
        }

        public async Task<AirconWebResponse> UpdateAsync()
        {
            return await _aircon.GetAsync("setSystemData?"
                + "airconOnOff=" + (Power ? "1" : "0")
                + "&fanSpeed=" + (int) FanSpeed
                + "&mode=" + (int) InverterMode
                + "&centralDesiredTemp=" + CentralDesiredTemp);
        }

        public async Task<SleepTimer> GetSleepTimerAsync()
        {
            var zoneTimer = await _aircon.GetAsync("getZoneTimer");
            return new SleepTimer(_aircon, zoneTimer.InnerResponse.Element("zoneTimer"));
        }
    }
}