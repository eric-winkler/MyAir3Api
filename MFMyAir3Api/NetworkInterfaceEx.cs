using System;
using System.Net;
using Microsoft.SPOT.Net.NetworkInformation;

namespace Winkler.MFMyAir3Api
{
    public static class NetworkInterfaceEx
    {
        public static IPAddress GetBroadcastAddress(this NetworkInterface networkInterface)
        {
            var adapterAddress = IPAddress.Parse(networkInterface.IPAddress);
            var subnetMask = IPAddress.Parse(networkInterface.SubnetMask);
            return adapterAddress.GetBroadcastAddress(subnetMask);
        }
    
        public static IPAddress GetBroadcastAddress(this IPAddress ip, IPAddress mask)
        {
            var ipAdressBytes = ip.GetAddressBytes();
            var subnetMaskBytes = mask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            var broadcastAddress = new byte[ipAdressBytes.Length];
            for (var i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }

            return new IPAddress(broadcastAddress);
        }
    }
}