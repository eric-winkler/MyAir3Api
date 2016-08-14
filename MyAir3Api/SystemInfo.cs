using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class SystemInfo
    {
        public int SystemType { get; private set; }
        public string Name { get; private set; }
        public string MyAppRevision { get; private set; }
        public bool HasUnitControl { get; private set; }
        public bool Dhcp { get; private set; }
        public string Ip { get; private set; }
        public string Netmask { get; private set; }
        public string Gateway { get; private set; }

        public SystemInfo(XElement data)
        {
            SystemType = int.Parse(data.Element("type").Value);
            Name = data.Element("name").Value;
            MyAppRevision = data.Element("MyAppRev").Value;
            HasUnitControl = int.Parse(data.Element("zoneStationHasUnitControl").Value) == 1;
            Dhcp = int.Parse(data.Element("dhcp").Value) == 1;
            Ip = data.Element("ip").Value;
            Netmask = data.Element("netmask").Value;
            Gateway = data.Element("gateway").Value;
        }
    }
}