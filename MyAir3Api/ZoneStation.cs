using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class ZoneStation
    {
        private readonly IAirconWebClient _aircon;

        //<zs103TechSettings>
        //  <numberofConstantZones>2</numberofConstantZones>
        //  <zsConstantZone1>1</zsConstantZone1>
        //  <zsConstantZone2>2</zsConstantZone2>
        //  <zsConstantZone3>0</zsConstantZone3>
        //  <tempSensorSelect>0</tempSensorSelect>
        //  <returnAirOffset>2.0</returnAirOffset>
        //  <controlZoneNumber>0</controlZoneNumber>
        //  <newAirFitted>0</newAirFitted>
        //  <ACinfo>1</ACinfo>
        //  <systemID>16</systemID>
        //</zs103TechSettings>

        public int SystemType { get; private set; }
        public string Name { get; private set; }
        public string MyAppRevision { get; private set; }
        public bool HasUnitControl { get; private set; }
        public bool Dhcp { get; private set; }
        public string Ip { get; private set; }
        public string Netmask { get; private set; }
        public string Gateway { get; private set; }

        public UnitControl UnitControl { get; private set; }
        public XElement Zs103TechSettings { get; private set; }

        public ZoneStation(IAirconWebClient aircon, XElement data)
        {
            _aircon = aircon;

            SystemType = int.Parse(data.Element("type").Value);
            Name = data.Element("name").Value;
            MyAppRevision = data.Element("MyAppRev").Value;
            HasUnitControl = int.Parse(data.Element("zoneStationHasUnitControl").Value) == 1;
            Dhcp = int.Parse(data.Element("dhcp").Value) == 1;
            Ip = data.Element("ip").Value;
            Netmask = data.Element("netmask").Value;
            Gateway = data.Element("gateway").Value;

            UnitControl = new UnitControl(_aircon, data.Element("unitcontrol"));
            Zs103TechSettings = data.Element("zs103TechSettings");
        }
    }
}