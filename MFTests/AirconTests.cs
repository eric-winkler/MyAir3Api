using MFUnit;
using Winkler.MFMyAir3Api.Tests.Infrastructure;

namespace Winkler.MFMyAir3Api.Tests
{
    public class AirconTests
    {
        public void CanConnectToAircon()
        {
            var testClient = new TestAirconWebClient(ReferenceResponses.GetSystemData);
            var zoneStation = Aircon.ConnectZoneStation(testClient);

            Assert.IsTrue(zoneStation != null);
            Assert.AreEqual(InverterMode.FanOnly, zoneStation.InverterMode);
            Assert.AreEqual(FanSpeed.Low, zoneStation.FanSpeed);
        } 
    }
}