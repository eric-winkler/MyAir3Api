using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Winkler.MyAir3Api
{
    public class AirconLocater
    {
        private static IUdpIdentifier _udpIdentifier;

        public AirconLocater() : this(new UdpIdentifier())
        {
        }

        internal AirconLocater(IUdpIdentifier udpIdentifier)
        {
            _udpIdentifier = udpIdentifier;
        }

        public async Task<Uri> LocateAsync()
        {
            var retriesLeft = 3;
            var ip = "";
            var ipLocated = false;

            while (retriesLeft > 0 && !ipLocated)
            {
                retriesLeft--;
                var response = await _udpIdentifier.IdentifyAirconAsync();
                ipLocated = TryParseAirconReply(response, out ip);
            }

            return ipLocated ? new Uri("http://" + ip) : null;
        }

        private static bool TryParseAirconReply(string response, out string ip)
        {
            ip = null;
            if (string.IsNullOrEmpty(response))
                return false;

            var airconReply = XElement.Parse(response);
            var systemElement = airconReply.Element("system");
            if (systemElement == null)
                return false;

            var ipElement = systemElement.Element("ip");
            if (ipElement == null)
                return false;

            ip = ipElement.Value;
            return ip != null;
        }
    }
}