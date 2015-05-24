using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;

namespace Winkler.MyAir3Api
{
    internal class UdpIdentifier : IUdpIdentifier
    {
        private const string ListenPort = "3001";
        private const string SendPort = "3000";
        private static readonly HostName BroadcastHostname = new HostName("255.255.255.255");
        private readonly byte[] _datagram = { 0x69, 0x64, 0x65, 0x6e, 0x74, 0x69, 0x66, 0x79 }; // "identify"

        public async Task<string> IdentifyAirconAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            using (var listenSocket = new DatagramSocket())
            {
                listenSocket.MessageReceived += (sender, args) =>
                                                {
                                                    var reader = args.GetDataReader();
                                                    tcs.SetResult(reader.ReadString(reader.UnconsumedBufferLength));
                                                };
                await listenSocket.BindServiceNameAsync(ListenPort);
                
                var outputStream = await listenSocket.GetOutputStreamAsync(BroadcastHostname, SendPort);
                await outputStream.WriteAsync(_datagram.AsBuffer());

                return await tcs.Task;
            }
        }
    }
}