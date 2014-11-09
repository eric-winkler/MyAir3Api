using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.SPOT.Net.NetworkInformation;

namespace Winkler.MFMyAir3Api
{
    internal class UdpIdentifier : IUdpIdentifier
    {
        private readonly IPEndPoint _listenEndpoint = new IPEndPoint(IPAddress.Any, 3001);
        private readonly IPEndPoint _sendEndpoint;
        private readonly byte[] _datagram = { 0x69, 0x64, 0x65, 0x6e, 0x74, 0x69, 0x66, 0x79 }; // "identify"

        public UdpIdentifier()
        {
            var broadcastAddress = NetworkInterface.GetAllNetworkInterfaces()[0].GetBroadcastAddress();
            _sendEndpoint = new IPEndPoint(broadcastAddress, 3000);
        }

        public string IdentifyAircon()
        {
            using (var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                listenSocket.Bind(_listenEndpoint);
                listenSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                listenSocket.SendTo(_datagram, _sendEndpoint);

                if (!listenSocket.Poll(1000000, SelectMode.SelectRead))
                    return null;

                var buffer = new byte[512];
                var bytesRead = listenSocket.Receive(buffer, buffer.Length, SocketFlags.None);
                return new string(Encoding.UTF8.GetChars(buffer, 0, bytesRead));
            }
        }
    }
}