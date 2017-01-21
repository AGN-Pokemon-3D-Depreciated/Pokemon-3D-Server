using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace Pokemon_3D_Server_Core.Server.Game.Server
{
    [Serializable]
    public class TcpClientCollection : Dictionary<TcpClient, Networking>, IDisposable
    {
        protected ICollection Collection;

        public TcpClientCollection()
        {
            Collection = this;
        }

        protected TcpClientCollection(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Collection = this;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public void Add(TcpClient tcpClient)
        {
            lock (Collection.SyncRoot)
                Add(tcpClient, new Networking(tcpClient));
        }

        public new void Remove(TcpClient obj)
        {
            lock (Collection.SyncRoot)
                base.Remove(obj);
        }

        public List<Networking> ActivePlayer
        {
            get
            {
                lock (Collection.SyncRoot)
                    return this.Where(a => a.Value.Player != null).Select(a => a.Value).ToList();
            }
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

        public void SendToAllPlayer(Package.Package package, bool exceptSelf = false)
        {
            lock (Collection.SyncRoot)
            {
                foreach (Networking network in ActivePlayer)
                {
                    if (exceptSelf)
                    {
                        if (package.Network != null && package.Network != network)
                            network.SentToPlayer(package);
                    }
                    else
                        network.SentToPlayer(package);
                }
            }
        }

        public void SendToAllOperator(Package.Package package)
        {
            lock (Collection.SyncRoot)
            {
                foreach (Networking network in ActivePlayer)
                {
                    if (package.Network != null && package.Network != network)
                        network.SentToPlayer(package);
                }
            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (KeyValuePair<TcpClient, Networking> item in this)
                        item.Value.Dispose();

                    Clear();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}