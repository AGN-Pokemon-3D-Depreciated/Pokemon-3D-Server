using System.Collections.Generic;
using System.Net.Sockets;

namespace Pokemon_3D_Server_Core.Server.Game.Server
{
    public class TcpClientCollection : Dictionary<TcpClient, Networking>
    {
        /// <summary>
        /// Add new TcpClient into the collection.
        /// </summary>
        public void Add(TcpClient TcpClient)
        {
            Add(TcpClient, new Networking(TcpClient));
        }

        /// <summary>
        /// Remove TcpClient from the collection.
        /// </summary>
        /// <param name="TcpClient">TcpClient.</param>
        public new void Remove(TcpClient TcpClient)
        {
            base.Remove(TcpClient);
        }
    }
}
