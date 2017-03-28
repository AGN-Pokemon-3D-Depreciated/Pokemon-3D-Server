using Amib.Threading;
using Modules.System;
using Modules.System.Net;
using Pokemon_3D_Server_Launcher_Core.Modules.System.Threading;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static Pokemon_3D_Server_Launcher_Game_Module.Server.Package.Package;

namespace Pokemon_3D_Server_Launcher_Game_Module.Server
{
    public class TcpListener : IDisposable
    {
        private Core Core;
        private ThreadHelper Thread = new ThreadHelper();
        private IWorkItemsGroup ThreadPool = new SmartThreadPool().CreateWorkItemsGroup(Environment.ProcessorCount);
        private System.Net.Sockets.TcpListener Listener;
        private bool IsActive = false;

        public DateTime StartTime { get; private set; }

        private int ErrorCount = 0;

        public TcpListener(Core core)
        {
            Core = core;
            Core.Logger.Log("TcpListener Initialized.");
        }

        public void Start()
        {
            try
            {
                StartTime = DateTime.Now;
                Listener = new System.Net.Sockets.TcpListener(new IPEndPoint(IPAddress.Any, Core.Settings.Server.Port));
                Listener.Start();
                StartListening();
            }
            catch (Exception ex)
            {
                ex.CatchError();
                Dispose();
            }
        }

        private async void StartListening()
        {
            try
            {
                string publicAddress = await IPAddressHelper.GetPublicIPAsync();
                string privateAddress = await IPAddressHelper.GetPrivateIPAsync();

                if (string.IsNullOrEmpty(publicAddress) || string.IsNullOrEmpty(privateAddress))
                {
                    Core.Logger.Log("Network is not available.", "Warning");
                    Dispose();
                }
                else
                {
                    if (Core.Settings.Server.GameModes.OfflineMode)
                        Core.Logger.Log("Players with offline profile can join the server.");

                    Thread.Add(() =>
                    {
                        IsActive = true;

                        do
                        {
                            try
                            {
                                TcpClient client = Listener.AcceptTcpClient();

                                ThreadPool.QueueWorkItem(() =>
                                {
                                    if (client != null)
                                        Core.TcpClientCollection.Add(client);
                                });
                            }
                            catch (ThreadAbortException) { return; }
                            catch (Exception) { }
                        } while (IsActive);
                    });

                    if (CheckPortOpen(publicAddress))
                    {
                        Core.Logger.Log($"Server started. Players can join using the following address: {publicAddress}:{Core.Settings.Server.Port.ToString()} (Global), {privateAddress}:{Core.Settings.Server.Port.ToString()} (Local) and with the following GameMode: {Core.Settings.Server.GameModes.ToString()}.");

                        if (Core.Settings.Server.CheckPort)
                            StartPortCheck(publicAddress);
                    }
                    else
                    {
                        Core.Logger.Log($"The specific port {Core.Settings.Server.Port.ToString()} is not opened. External/Global IP will not accept new players.");
                        Core.Logger.Log($"Server started. Players can join using the following address: {privateAddress}:{Core.Settings.Server.Port.ToString()} (Local) and with the following GameMode: {Core.Settings.Server.GameModes.ToString()}.");
                    }
                }
            }
            catch (Exception)
            {
                Dispose();
            }
        }

        private bool CheckPortOpen(string publicAddress)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.SendTimeout = 10000;
                    tcpClient.ReceiveTimeout = 10000;
                    tcpClient.ConnectAsync(publicAddress, Core.Settings.Server.Port).Wait(10000);

                    if (tcpClient.GetStream() != null)
                    {
                        using (StreamWriter writer = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true })
                        {
                            using (StreamReader reader = new StreamReader(tcpClient.GetStream()))
                            {
                                writer.WriteLine(new Package.Package(Core, PackageTypes.ServerDataRequest, ""));

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

        private void StartPortCheck(string publicAddress)
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
                            if (CheckPortOpen(publicAddress))
                            {
                                Core.Logger.Log("Port check cycle completed. Result: True.");
                                sw.Restart();
                                ErrorCount = 0;
                            }
                            else
                            {
                                Core.Logger.Log("Port check cycle completed. Result: False.");
                                sw.Restart();
                                ErrorCount++;
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

        public void Dispose()
        {
            IsActive = false;

            if (Listener != null) Listener.Stop();
            if (Thread.Count > 0) Thread.Dispose();
            if (Core.TcpClientCollection.Count > 0) Core.TcpClientCollection.Dispose();

            Core.Logger.Log("TcpListener Disposed.");
        }
    }
}
