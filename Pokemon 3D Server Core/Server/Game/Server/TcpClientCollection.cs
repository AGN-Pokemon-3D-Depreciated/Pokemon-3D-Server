using Pokemon_3D_Server_Core.Modules.System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Pokemon_3D_Server_Core.Server.Game.Server
{
    public class TcpClientCollection : DictionaryHelper<TcpClient, Networking>, IDisposable
    {
        /// <summary>
        /// Get Active player count.
        /// </summary>
        public int ActivePlayerCount { get { return this.Where(a => a.Value.Player != null).Count(); } }

        /// <summary>
        /// Add new TcpClient into the collection.
        /// </summary>
        /// <param name="tcpClient">TcpClient.</param>
        public void Add(TcpClient tcpClient)
        {
            lock (Collection.SyncRoot)
                Add(tcpClient, new Networking(tcpClient));
        }

        /// <summary>
        /// Get the next player id.
        /// </summary>
        public int NextPlayerID()
        {
            if (ActivePlayerCount == 0)
                return 0;
            else
            {
                int index = 0;
                Networking[] activePlayerList = Values.Where(a => a.Player != null).OrderBy(a => a.Player.ID).ToArray();

                for (int i = 0; i < activePlayerList.Count(); i++)
                {
                    if (activePlayerList[i].Player.ID == index)
                        index++;
                    else
                        return index;
                }

                return ActivePlayerCount;
            }
        }

        /// <summary>
        /// Dispose TcpClient and its network.
        /// </summary>
        public void Dispose()
        {
            int actualLength = Keys.Length;

            for (int i = 0; i < actualLength; i++)
                Values[0].Dispose();
        }
    }
}
