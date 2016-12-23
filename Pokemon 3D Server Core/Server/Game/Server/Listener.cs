using Amib.Threading;
using Modules.System;
using Modules.System.Net;
using Modules.System.Threading;
using Pokemon_3D_Server_Core.Interface;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static Pokemon_3D_Server_Core.Settings.Logger;

namespace Pokemon_3D_Server_Core.Server.Game.Server
{
    public class Listener : IModules
    {
        private TcpListener TcpListener;
        private ThreadHelper Thread = new ThreadHelper();
        private IWorkItemsGroup ThreadPool = new SmartThreadPool().CreateWorkItemsGroup(Environment.ProcessorCount);
        private bool IsActive = false;

        /// <summary>
        /// Get the name of the module.
        /// </summary>
        public string Name { get { return "Server Listener"; } }

        /// <summary>
        /// Get the version of the module.
        /// </summary>
        public string Version { get { return "0.54"; } }

        /// <summary>
        /// Start the module.
        /// </summary>
        public void Start()
        {
            try
            {
                if (IPAddressHelper.GetPublicIP() == null)
                {
                    Core.Logger.Log("Network is not available.", LogTypes.Warning);
                    Stop();
                }
                else
                {
                    TcpListener = new TcpListener(new IPEndPoint(IPAddress.Any, Core.Settings.Server.Port));
                    TcpListener.Start();

                    Thread.Add(() =>
                    {
                        Core.Logger.Log("Pokemon 3D Listener initialized.");
                        IsActive = true;

                        do
                        {
                            try
                            {
                                TcpClient Client = TcpListener.AcceptTcpClient();

                                ThreadPool.QueueWorkItem(() =>
                                {
                                    if (Client != null)
                                        Core.TcpClientCollection.Add(Client);
                                });
                            }
                            catch (ThreadAbortException) { return; }
                            catch (Exception) { }
                        } while (IsActive);
                    });

                    if (Core.Settings.Server.OfflineMode || Core.Settings.Server.GameMode.NeedOfflineMode())
                        Core.Logger.Log("Players with offline profile can join the server.");

                    string GameMode = string.Join(", ", Core.Settings.Server.GameMode.GameMode.ToArray());

                    // Do some wonderful magic here.
                }
            }
            catch (Exception ex)
            {
                ex.CatchError();
                Stop();
            }
        }

        /// <summary>
        /// Stop the module.
        /// </summary>
        public void Stop()
        {
            IsActive = false;

            if (TcpListener != null) TcpListener.Stop();
            if (Thread.Count > 0) Thread.Dispose();
            //if (Core.PlayerCollection != null) Core.PlayerCollection.Dispose();
        }
    }
}
