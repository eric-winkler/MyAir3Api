using System;

namespace Winkler.MFMyAir3Api
{
    public class ZoneStation
    {
        private readonly IAirconWebClient _aircon;

        public bool PowerOn { get; set; }
        public FanSpeed FanSpeed { get; set; }
        public InverterMode InverterMode { get; set; }
        public bool TempsSetting { get; private set; }
        public double CentralActualTemp { get; private set; }
        public double CentralDesiredTemp { get; set; }
        public string ErrorCode { get; private set; }
        public string ActivationCodeStatus { get; private set; }
        public int NumberOfZones { get; private set; }
        public double MaxUserTemp { get; private set; }
        public double MinUserTemp { get; private set; }
        public int NumberOfSchedules { get; private set; }

        //public SystemInfo SystemInfo { get; private set; }
        //public Zs103TechSettings Zs103TechSettings { get; private set; }

        public ZoneStation(IAirconWebClient aircon, XElement data)
        {
            _aircon = aircon;

            //SystemInfo = new SystemInfo(data);
            //Zs103TechSettings = new Zs103TechSettings(data.Element("zs103TechSettings"));
            
            var unitControlElement = data.Element("unitcontrol");
            PowerOn = int.Parse(unitControlElement.Element("airconOnOff").Value) == 1;
            FanSpeed = (FanSpeed)int.Parse(unitControlElement.Element("fanSpeed").Value);
            InverterMode = (InverterMode)int.Parse(unitControlElement.Element("mode").Value);
            TempsSetting = int.Parse(unitControlElement.Element("unitControlTempsSetting").Value) == 1;
            CentralActualTemp = double.Parse(unitControlElement.Element("centralActualTemp").Value);
            CentralDesiredTemp = double.Parse(unitControlElement.Element("centralDesiredTemp").Value);
            ErrorCode = unitControlElement.Element("airConErrorCode").Value;
            ActivationCodeStatus = unitControlElement.Element("activationCodeStatus").Value;
            NumberOfZones = int.Parse(unitControlElement.Element("numberOfZones").Value);
            MaxUserTemp = double.Parse(unitControlElement.Element("maxUserTemp").Value);
            MinUserTemp = double.Parse(unitControlElement.Element("minUserTemp").Value);
            NumberOfSchedules = int.Parse(unitControlElement.Element("availableSchedules").Value);
        }

        //public async Task<IEnumerable<Zone>> GetZonesAsync()
        //{
        //    var zoneRetrievalTasks = Enumerable.Range(1, NumberOfZones)
        //        .Select(z => new
        //        {
        //            ZoneId = z,
        //            ZoneTask = _aircon.GetAsync("getZoneData?zone=" + z)
        //        }).ToArray();
        //    await Task.WhenAll(zoneRetrievalTasks.Select(z => z.ZoneTask));

        //    return zoneRetrievalTasks.Select(z => new Zone(_aircon, z.ZoneId, z.ZoneTask.Result.InnerResponse));
        //}

        //public async Task<IEnumerable<Schedule>> GetSchedulesAsync()
        //{
        //    var scheduleRetrievalTasks = Enumerable.Range(1, NumberOfSchedules)
        //        .Select(s => new
        //        {
        //            ScheduleId = s,
        //            ScheduleTask = _aircon.GetAsync("getScheduleData?schedule=" + s)
        //        }).ToArray();
        //    await Task.WhenAll(scheduleRetrievalTasks.Select(s => s.ScheduleTask));

        //    return scheduleRetrievalTasks.Select(s => new Schedule(_aircon, s.ScheduleId, s.ScheduleTask.Result.InnerResponse));
        //}

        //public async Task<AirconWebResponse> SyncSystemClockAsync()
        //{
        //    var now = DateTime.Now;
        //    return await _aircon.GetAsync("setClock?"
        //                                    + "hours=" + now.Hour
        //                                    + "&minutes=" + now.Minute
        //                                    + "&day=" + now.Day
        //                                    + "&month=" + now.Month
        //                                    + "&year=" + now.Year
        //                                    + "&dow=" + ToIntDayOfWeek(now.DayOfWeek));
        //}

        //public async Task<SleepTimer> GetSleepTimerAsync()
        //{
        //    var zoneTimer = await _aircon.GetAsync("getZoneTimer");
        //    return new SleepTimer(_aircon, zoneTimer.InnerResponse.Element("zoneTimer"));
        //}

        //public async Task<AirconWebResponse> UpdateAsync()
        //{
        //    return await _aircon.GetAsync("setSystemData?"
        //        + "airconOnOff=" + (PowerOn ? "1" : "0")
        //        + "&fanSpeed=" + (int)FanSpeed
        //        + "&mode=" + (int)InverterMode
        //        + "&centralDesiredTemp=" + CentralDesiredTemp);
        //}

        private static int ToIntDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;
                default:
                    throw new ArgumentException("Unrecognised day of week: " + dayOfWeek);
            }
        }
    }
}