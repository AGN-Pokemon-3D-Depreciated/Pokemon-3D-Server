using Amib.Threading;
using Modules.System;
using Modules.System.Net;
using Modules.System.Threading;
using Pokemon_3D_Server_Core.Interface;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static Pokemon_3D_Server_Core.Collections.LogTypeCollection;
using static Pokemon_3D_Server_Core.Server.Game.Server.Package.Package;

namespace Pokemon_3D_Server_Core.Server.Game.Server
{
    public class Listener : IModules, IDisposable
    {
        public string Name { get; } = "Game Server Listener";
        public string Version { get; } = "0.54";

        private ThreadHelper Thread = new ThreadHelper();
        private IWorkItemsGroup ThreadPool = new SmartThreadPool().CreateWorkItemsGroup(Environment.ProcessorCount);
        private TcpListener TcpListener;
        private bool IsActive = false;

        public DateTime StartTime { get; private set; } = DateTime.Now;

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
                    StartTime = DateTime.Now;
                    TcpListener = new TcpListener(new IPEndPoint(IPAddress.Any, Core.Settings.Server.Game.Network.Port));
                    TcpListener.AllowNatTraversal(true);
                    TcpListener.Start();

                    StartListening();

                    if (Core.Settings.Server.Game.Server.GameModes.OfflineMode)
                        Core.Logger.Log("Players with offline profile can join the server.");

                    if (CheckPortOpen())
                    {
                        Core.Logger.Log($"Server started. Players can join using the following address: {Core.Settings.Server.Game.Network.IPAddress}:{Core.Settings.Server.Game.Network.Port.ToString()} (Global), {IPAddressHelper.GetPrivateIP()}:{Core.Settings.Server.Game.Network.Port.ToString()} (Local) and with the following GameMode: {Core.Settings.Server.Game.Server.GameModes.ToString()}.");

                        if (Core.Settings.Server.Game.Network.CheckPort)
                            StartPortCheck();
                    }
                    else
                    {
                        Core.Logger.Log($"The specific port {Core.Settings.Server.Game.Network.Port.ToString()} is not opened. External/Global IP will not accept new players.");
                        Core.Logger.Log($"Server started. Players can join using the following address: {IPAddressHelper.GetPrivateIP()}:{Core.Settings.Server.Game.Network.Port.ToString()} (Local) and with the following GameMode: {Core.Settings.Server.Game.Server.GameModes.ToString()}.");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.CatchError();
                Stop();
            }
        }

        public void Stop()
        {
            IsActive = false;

            if (TcpListener != null) TcpListener.Stop();
            if (Thread.Count > 0) Thread.Dispose();
            if (Core.TcpClientCollection.GameTcpClientCollection.Count > 0) Core.TcpClientCollection.GameTcpClientCollection.Dispose();
        }

        private void StartListening()
        {
            Thread.Add(() =>
            {
                Core.Logger.Log("Pokemon 3D Listener initialized.");
                IsActive = true;

                do
                {
                    try
                    {
                        TcpClient client = TcpListener.AcceptTcpClient();

                        ThreadPool.QueueWorkItem(() =>
                        {
                            if (client != null)
                                Core.TcpClientCollection.GameTcpClientCollection.Add(client);
                        });
                    }
                    catch (ThreadAbortException) { return; }
                    catch (Exception) { }
                } while (IsActive);
            });
        }

        private void StartPortCheck()
        {
            Thread.Add(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Core.Logger.Log("Port check is now enabled.");

                do
                {
                    try
                    {
                        if (sw.Elapsed.TotalMinutes >= 15)
                        {
                            if (CheckPortOpen())
                            {
                                Core.Logger.Log("Port check cycle completed. Result: True.");
                                sw.Restart();
                            }
                            else
                            {
                                Core.Logger.Log("Port check cycle completed. Result: False.");
                                sw.Restart();
                            }
                        }
                        else
                            Thread.Sleep(1000);
                    }
                    catch (ThreadAbortException) { return; }
                    catch (Exception) { }
                } while (IsActive);
            });
        }

        private bool CheckPortOpen()
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.SendTimeout = 10000;
                    tcpClient.ReceiveTimeout = 10000;
                    tcpClient.ConnectAsync(Core.Settings.Server.Game.Network.IPAddress, Core.Settings.Server.Game.Network.Port).Wait(10000);

                    if (tcpClient.GetStream() != null)
                    {
                        using (StreamWriter writer = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true })
                        {
                            using (StreamReader reader = new StreamReader(tcpClient.GetStream()))
                            {
                                writer.WriteLine(new Package.Package(PackageTypes.ServerDataRequest, "", null));

                                if (string.IsNullOrWhiteSpace(reader.ReadLine()))
                                    return false;
                                else
                                    return true;
                            }
                        }
                    }
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
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
                    Thread.Dispose();
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