using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class Schedule
    {
        private readonly IAirconWebClient _aircon;

        public int Number { get; private set; }
        public string Name { get; set; }
        public ScheduledDay ScheduledDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<ZoneStatus> Zones { get; private set; }

        public Schedule(IAirconWebClient aircon, int number, XElement scheduleData)
        {
            _aircon = aircon;
            scheduleData = scheduleData.Element("schedule" + number);

            Number = number;
            Name = scheduleData.Element("name").Value;
            ScheduledDays = ParseScheduledDays(scheduleData.Element("weekDays"));

            var startTimeHours = int.Parse(scheduleData.Element("startTimeHours").Value);
            var startTimeMinutes = int.Parse(scheduleData.Element("startTimeMinutes").Value);
            StartTime = new TimeSpan(0, startTimeHours, startTimeMinutes, 0);

            var endTimeHours = int.Parse(scheduleData.Element("endTimeHours").Value);
            var endTimeMinutes = int.Parse(scheduleData.Element("endTimeMinutes").Value);
            EndTime = new TimeSpan(0, endTimeHours, endTimeMinutes, 0);

            Enabled = int.Parse(scheduleData.Element("scheduleStatus").Value) == 1;
            Zones = scheduleData.Element("zoneStatus").Elements().Select(e => new ZoneStatus(e)).ToArray();
        }

        public async Task<AirconWebResponse> UpdateAsync()
        {
            return await _aircon.GetAsync("setScheduleData?"
                + "schedule=" + Number
                + "&day=" + ToSetDaysString(ScheduledDays)
                + "&startHours=" + StartTime.Hours
                + "&startMinutes=" + StartTime.Minutes
                + "&endHours=" + EndTime.Hours
                + "&endMinutes=" + EndTime.Minutes
                + "&scheduleStatus=" + (Enabled ? "1" : "0")
                + "&zoneStatus=0"  // not sure what this is, always seems to be zero
                + "&zones=" + Zones.OrderBy(z => z.ZoneNumber).Aggregate("", (a,z) => a + (z.Enabled ? "1" : "0"))
                + "&name=" + HttpUtility.UrlEncode(Name));
        }

        private static string ToSetDaysString(ScheduledDay scheduledDays)
        {
            return (scheduledDays.HasFlag(ScheduledDay.Monday) ? "1" : "")
                   + (scheduledDays.HasFlag(ScheduledDay.Tuesday) ? "2" : "")
                   + (scheduledDays.HasFlag(ScheduledDay.Wednesday) ? "3" : "")
                   + (scheduledDays.HasFlag(ScheduledDay.Thursday) ? "4" : "")
                   + (scheduledDays.HasFlag(ScheduledDay.Friday) ? "5" : "")
                   + (scheduledDays.HasFlag(ScheduledDay.Saturday) ? "6" : "")
                   + (scheduledDays.HasFlag(ScheduledDay.Sunday) ? "7" : "");
        }

        private static ScheduledDay ParseScheduledDays(XElement element)
        {
            var monday = int.Parse(element.Element("Monday").Value) == 1;
            var tuesday = int.Parse(element.Element("Tuesday").Value) == 1;
            var wednesday = int.Parse(element.Element("Wednesday").Value) == 1;
            var thursday = int.Parse(element.Element("Thursday").Value) == 1;
            var friday = int.Parse(element.Element("Friday").Value) == 1;
            var saturday = int.Parse(element.Element("Saturday").Value) == 1;
            var sunday = int.Parse(element.Element("Sunday").Value) == 1;

            return (monday ? ScheduledDay.Monday : ScheduledDay.None)
                 | (tuesday ? ScheduledDay.Tuesday : ScheduledDay.None)
                 | (wednesday ? ScheduledDay.Wednesday : ScheduledDay.None)
                 | (thursday ? ScheduledDay.Thursday : ScheduledDay.None)
                 | (friday ? ScheduledDay.Friday : ScheduledDay.None)
                 | (saturday ? ScheduledDay.Saturday : ScheduledDay.None)
                 | (sunday ? ScheduledDay.Sunday : ScheduledDay.None);
        }
    }
}

