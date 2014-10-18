MyAir3Api
=========

The MyAir3Api provides a .net consumable interface for interacting with the airconditioner control functions exposed by AdvantageAir's MyAir3 controller.


## Example Usage

### Turn on, max fans and cool to 22 degrees

    var zoneStation = await Aircon.ConnectAsync(new Uri("http://10.0.0.100"));
    zoneStation.CentralDesiredTemp = 22m;
    zoneStation.InverterMode = InverterMode.Cool;
	zoneStation.FanSpeed = FanSpeed.High;
	zoneStation.PowerOn = true;
	await zoneStation.UpdateAsync();


### Turn on the master bedroom zone, and turn everything else off

    var zoneStation = await Aircon.ConnectAsync(new Uri("http://10.0.0.100"));
	foreach(var zone in await zoneStation.GetZonesAsync())
	{
		zone.Enabled = zone.Name == "MASTER BED";
		await zone.UpdateAsync();
	}


### Clear all the schedules, then set a new schedule for weekends, 0500-0600

    var zoneStation = await Aircon.ConnectAsync(new Uri("http://10.0.0.100"));
	var schedules = await zoneStation.GetSchedulesAsync();
	await Task.WhenAll(schedules.Select (s => s.DisableAsync()));

	var earlyMorningSchedule = schedules.First();
	earlyMorningSchedule.Name = "Comfy Morning";
	earlyMorningSchedule.ScheduledDays = ScheduledDay.Saturday | ScheduledDay.Sunday;
	earlyMorningSchedule.StartTime = new TimeSpan(5,0,0);
	earlyMorningSchedule.EndTime = new TimeSpan(6,0,0);
	foreach(var zone in earlyMorningSchedule.Zones)
	{
		zone.Enabled = true;
	}
	await earlyMorningSchedule.UpdateAsync();


## License

MyAir3Api is released under the Apache License (see the [license file](LICENSE)) and is copyright Eric Winkler, 2014.