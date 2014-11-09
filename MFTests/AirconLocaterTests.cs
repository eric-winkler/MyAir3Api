using MFUnit;
using Winkler.MFMyAir3Api.Tests.Infrastructure;

namespace Winkler.MFMyAir3Api.Tests
{
    public class AirconLocaterTests
    {
        public void CanLocateAircon()
        {
            var locater = new AirconLocater(new TestUdpIdentifier());

            var location = locater.Locate();

            Assert.AreEqual("http://10.0.0.1/", location.AbsoluteUri);
        }

        class TestUdpIdentifier : IUdpIdentifier
        {
            public string IdentifyAircon()
            {
                return ReferenceResponses.LocateAircon;
            }
        }
    }
}