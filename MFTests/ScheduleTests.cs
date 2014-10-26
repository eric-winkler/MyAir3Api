using MFUnit;
using Winkler.MFMyAir3Api.Tests.Infrastructure;

namespace Winkler.MFMyAir3Api.Tests
{
    public class ScheduleTests
    {
        private const int ScheduleNumber = 2;

        public void CanParseSchedule()
        {
            var testClient = new TestAirconWebClient(ReferenceResponses.GetScheduleData);
            var xelement = XElement.Parse(ReferenceResponses.GetScheduleData);

            var schedule = new Schedule(testClient, ScheduleNumber, xelement);

            Assert.AreEqual("PROGRAM2", schedule.Name);
            Assert.AreEqual(6, schedule.Zones.Length);
        }

        public void CanUpdateSchedule()
        {
            var testClient = new TestAirconWebClient(ReferenceResponses.GetScheduleData);
            var xelement = XElement.Parse(ReferenceResponses.GetScheduleData);
            var schedule = new Schedule(testClient, ScheduleNumber, xelement);

            schedule.Name = "TESTNAME";
            schedule.Zones[3].Enabled = true;
            schedule.Update();

            Assert.AreEqual("setScheduleData?schedule=2&day=&startHours=0&startMinutes=0&endHours=0&endMinutes=0&scheduleStatus=0&zoneStatus=0&zones=000100&name=TESTNAME", testClient.LastRequestUri);
        }
    }
}