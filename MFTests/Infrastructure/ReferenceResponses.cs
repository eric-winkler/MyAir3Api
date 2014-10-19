namespace Winkler.MFMyAir3Api.Tests.Infrastructure
{
    public class ReferenceResponses
    {
        public const string GetSystemData = @"<?xml version=""1.0"" encoding=""UTF-8"" ?><iZS10.3><request>getSystemData</request><mac>000000000000</mac><authenticated>1</authenticated><system><type>12</type><name>MYAIR3</name><MyAppRev>7.4</MyAppRev><zoneStationHasUnitControl>1</zoneStationHasUnitControl><dhcp>1</dhcp><ip>10.0.0.10</ip><netmask>255.255.255.0</netmask><gateway>10.0.0.1</gateway><unitcontrol><airconOnOff>0</airconOnOff><fanSpeed>1</fanSpeed><mode>3</mode><unitControlTempsSetting>0</unitControlTempsSetting><centralActualTemp>21.6</centralActualTemp><centralDesiredTemp>26.0</centralDesiredTemp><airConErrorCode>    </airConErrorCode><activationCodeStatus>0</activationCodeStatus><numberOfZones>6</numberOfZones><maxUserTemp>32.0</maxUserTemp><minUserTemp>16.0</minUserTemp><availableSchedules>8</availableSchedules></unitcontrol><zs103TechSettings><numberofConstantZones>1</numberofConstantZones><zsConstantZone1>1</zsConstantZone1><zsConstantZone2>0</zsConstantZone2><zsConstantZone3>0</zsConstantZone3><tempSensorSelect>0</tempSensorSelect><returnAirOffset>0.0</returnAirOffset><controlZoneNumber>0</controlZoneNumber><newAirFitted>0</newAirFitted><ACinfo>1</ACinfo><systemID>16</systemID></zs103TechSettings></system></iZS10.3>";


    }
}