using Pokemon_3D_Server_Core.Server.Game.Server;
using System;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;

namespace Modules.System.Net
{
    public static class IPAddressHelper
    {
        /// <summary>
        /// Get the public IP of the hosting computer.
        /// </summary>
        public static string GetPublicIP()
        {
            using (WebClient Client = new WebClient())
            {
                try
                {
                    Client.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
                    return Client.DownloadString("http://api.ipify.org");
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get the public IP of the client.
        /// </summary>
        /// <param name="network">Network.</param>
        public static string GetPublicIPFromClient(this Networking network)
        {
            return ((IPEndPoint)network.TcpClient.Client.RemoteEndPoint).Address.ToString();
        }

        /// <summary>
        /// Get the private IP of the hosting computer.
        /// </summary>
        public static string GetPrivateIP()
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                return host.AddressList?.Where(a => a.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault()?.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
