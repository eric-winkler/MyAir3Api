using MFUnit;
using Winkler.MFMyAir3Api.Tests.Infrastructure;

namespace Winkler.MFMyAir3Api.Tests
{
    public class ScheduleTests
    {
        public void CanParseSchedule()
        {
            var testClient = new TestAirconWebClient(ReferenceResponses.GetScheduleData);
            var xelement = XElement.Parse(ReferenceResponses.GetScheduleData);
            
            var schedule = new Schedule(testClient, 2, xelement);

            Assert.AreEqual("PROGRAM2", schedule.Name);
            Assert.AreEqual(6, schedule.Zones.Length);
        } 
    }
}