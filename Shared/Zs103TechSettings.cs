using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class Zs103TechSettings
    {
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

        public XElement Data { get; private set; }

        public Zs103TechSettings(XElement element)
        {
            Data = element;
        }
    }
}