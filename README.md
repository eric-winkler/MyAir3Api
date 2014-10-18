MyAir3Api
=========

The MyAir3Api provides a .net consumable interface for interacting with the airconditioner control functions exposed by AdvantageAir's MyAir3 controller.


## Example Usage

### Turn on, max fans and cool to 22 degrees

    var zoneStation = await Aircon.ConnectAsync(new Uri("http://10.0.0.100"));
    zoneStation.UnitControl.CentralDesiredTemp = 22m;
    zoneStation.UnitControl.InverterMode = InverterMode.Cool;
    zoneStation.UnitControl.FanSpeed = FanSpeed.High;
    zoneStation.UnitControl.PowerOn = true;
    await zoneStation.UnitControl.UpdateAsync();


## License

MyAir3Api is released under the Apache License (see the [license file](LICENSE)) and is copyright Eric Winkler, 2014.
