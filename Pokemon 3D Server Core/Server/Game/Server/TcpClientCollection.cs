using Pokemon_3D_Server_Core.Modules.System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Pokemon_3D_Server_Core.Server.Game.Server
{
    public class TcpClientCollection : DictionaryHelper<TcpClient, Networking>, IDisposable
    {
        public List<Networking> ActivePlayer { get { return this.Where(a => a.Value.Player != null).Select(a => a.Value).ToList(); } }

        public void Add(TcpClient tcpClient)
        {
            lock (Collection.SyncRoot)
                Add(tcpClient, new Networking(tcpClient));
        }

        public int NextPlayerID()
        {
            lock (Collection.SyncRoot)
            {
                if (ActivePlayer.Count() == 0)
                    return 0;
                else
                {
                    int index = 0;
                    for (int i = 0; i < ActivePlayer.Count(); i++)
                    {
                        if (ActivePlayer[i].Player.ID == index)
                            index++;
                        else
                            return index;
                    }

                    return ActivePlayer.Count();
                }
            }
        }

        public void UpdateWorld()
        {
            lock (Collection.SyncRoot)
            {
                foreach (Networking network in ActivePlayer)
                    network.Player.CanUpdateWorld = true;
            }
        }

        public void Dispose()
        {
            int actualLength = Keys.Length;

            for (int i = 0; i < actualLength; i++)
                Values[0].Dispose();
        }
    }
}
