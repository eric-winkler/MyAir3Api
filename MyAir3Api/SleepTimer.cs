using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public enum SleepTimerStatus
    {
        Disabled = 0,
        CountingDownToShutoff = 2
    }

    public class SleepTimer
    {
        private readonly IAirconWebClient _aircon;

        public TimeSpan TimeRemaining { get; set; }
        public SleepTimerStatus Status { get; set; }

        public SleepTimer(IAirconWebClient aircon, XElement timerData)
        {
            _aircon = aircon;

            var startTimeHours = int.Parse(timerData.Element("startTimeHours").Value);
            var startTimeMinutes = int.Parse(timerData.Element("startTimeMinutes").Value);
            var endTimeHours = int.Parse(timerData.Element("endTimeHours").Value);
            var endTimeMinutes = int.Parse(timerData.Element("endTimeMinutes").Value);
            TimeRemaining = new TimeSpan(0, endTimeHours, endTimeMinutes, 0) - new TimeSpan(0, startTimeHours, startTimeMinutes, 0);
            Status = (SleepTimerStatus)int.Parse(timerData.Element("scheduleStatus").Value);
        }

        public async Task<AirconWebResponse> UpdateAsync()
        {
            return await _aircon.GetAsync("setZoneTimer?"
                + "&startTimeHours=" + DateTime.Now.Hour
                + "&startTimeMinutes=" + DateTime.Now.Minute
                + "&endTimeHours=" + DateTime.Now.Add(TimeRemaining).Hour
                + "&endTimeMinutes=" + DateTime.Now.Add(TimeRemaining).Minute
                + "&scheduleStatus=" + (int) Status);
        }
    }
}