using MFUnit;
using Winkler.MFMyAir3Api.Tests.Infrastructure;

namespace Winkler.MFMyAir3Api.Tests
{
    public class ZoneStationTests
    {
        public void CanConnectToAircon()
        {
            var testClient = new TestAirconWebClient(ReferenceResponses.GetSystemData);
            var zoneStation = Aircon.ConnectZoneStation(testClient);

            Assert.IsTrue(zoneStation != null);
            Assert.AreEqual(InverterMode.FanOnly, zoneStation.InverterMode);
            Assert.AreEqual(FanSpeed.Low, zoneStation.FanSpeed);
        }

        public void CanUpdateZoneStation()
        {
            var testClient = new TestAirconWebClient(ReferenceResponses.GetSystemData);
            var zoneStation = Aircon.ConnectZoneStation(testClient);
            zoneStation.PowerOn = true;
            zoneStation.FanSpeed = FanSpeed.Medium;
            zoneStation.InverterMode = InverterMode.FanOnly;
            zoneStation.CentralDesiredTemp = 21;

            zoneStation.Update();

            Assert.AreEqual("setSystemData?airconOnOff=1&fanSpeed=2&mode=3&centralDesiredTemp=21", testClient.LastRequestUri);
        }
    }
}