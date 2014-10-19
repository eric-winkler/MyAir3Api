using MFUnit;
using Winkler.MFMyAir3Api.Tests.Infrastructure;

namespace Winkler.MFMyAir3Api.Tests
{
    public class AirconWebResponseTests
    {
        public void CanExtractAuthenticationState()
        {
            var airconWebResponse = AirconWebResponse.Parse(ReferenceResponses.GetSystemData);

            Assert.IsTrue(airconWebResponse.IsAuthenticated);
        }
    }
}