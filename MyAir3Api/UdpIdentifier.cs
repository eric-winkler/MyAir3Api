using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Winkler.MyAir3Api
{
    internal class UdpIdentifier : IUdpIdentifier
    {
        private readonly IPEndPoint _listenEndpoint = new IPEndPoint(IPAddress.Any, 3001);
        private readonly IPEndPoint _sendEndpoint = new IPEndPoint(IPAddress.Broadcast, 3000);
        private readonly byte[] _datagram = Encoding.ASCII.GetBytes("identify");

        public async Task<string> IdentifyAirconAsync()
        {
            using (var udpClient = new UdpClient(_listenEndpoint))
            {
                var listenTask = udpClient.ReceiveAsync();
                await udpClient.SendAsync(_datagram, _datagram.Length, _sendEndpoint).ConfigureAwait(false);
                await Task.WhenAny(listenTask, Task.Delay(1000)).ConfigureAwait(false);

                return listenTask.IsCompleted
                    ? Encoding.ASCII.GetString(listenTask.Result.Buffer)
                    : null;
            }
        }
    }
}