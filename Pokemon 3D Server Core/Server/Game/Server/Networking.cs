using Amib.Threading;
using Modules.System;
using Modules.System.Threading;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace Pokemon_3D_Server_Core.Server.Game.Server
{
    public class Networking : IDisposable
    {
        /// <summary>
        /// Get TcpClient.
        /// </summary>
        public TcpClient TcpClient { get; private set; }

        /// <summary>
        /// Get/Set Player.
        /// </summary>
        public Player.Player Player { get; set; }

        /// <summary>
        /// Get Network activity.
        /// </summary>
        public bool IsActive { get; private set; } = false;

        private StreamReader Reader;
        private StreamWriter Writer;
        private ThreadHelper Thread = new ThreadHelper();
        private IWorkItemsGroup ThreadPool = new SmartThreadPool().CreateWorkItemsGroup(Environment.ProcessorCount);
        private IWorkItemsGroup ThreadPool2 = new SmartThreadPool().CreateWorkItemsGroup(1);

        public Networking(TcpClient tcpClient)
        {
            TcpClient = tcpClient;

            Reader = new StreamReader(tcpClient.GetStream());
            Writer = new StreamWriter(tcpClient.GetStream());

            IsActive = true;

            Thread.Add(() =>
            {
                int errorCount = 0;

                do
                {
                    try
                    {
                        if (tcpClient.Available > -1)
                        {
                            string packageString = Reader.ReadLine();

                            if (!string.IsNullOrWhiteSpace(packageString))
                            {
                                ThreadPool.QueueWorkItem(() =>
                                {
                                    Package.Package package = new Package.Package(packageString, this);

                                    if (package.IsValid)
                                        package.Handle();

                                    Core.Logger.Debug($"Receive: {packageString}", this);
                                });

                                errorCount = 0;
                            }
                            else
                            {
                                errorCount++;

                                if (errorCount > 10)
                                {
                                    Core.Logger.Debug("Too much error received from the client.", this);
                                    throw new Exception("Too much error received from the client.");
                                }
                            }
                        }
                        else
                        {
                            Core.Logger.Debug("Unexpected error occured.", this);
                            throw new Exception("Unexpected error occured.");
                        }
                    }
                    catch (Exception)
                    {
                        if (IsActive)
                        {
                            IsActive = false;
                            Dispose();
                        }
                    }
                } while (IsActive);
            });
        }

        /// <summary>
        /// Sent To Player
        /// </summary>
        /// <param name="p">Package to send.</param>
        public void SentToPlayer(Package.Package p)
        {
            ThreadPool2.QueueWorkItem(() =>
            {
                try
                {
                    Writer.WriteLine(p.ToString());
                    Writer.Flush();
                    Core.Logger.Debug($"Sent: {p.ToString()}", this);
                }
                catch (Exception) { }
            });
        }

        /// <summary>
        /// Dispose Network.
        /// </summary>
        public void Dispose()
        {
            IsActive = false;
            ThreadPool2.WaitForIdle();

            Core.TcpClientCollection.Remove(TcpClient);

            if (TcpClient != null) TcpClient.Close();

            if (Reader != null) Reader.Dispose();
            if (Writer != null) Writer.Dispose();

            Thread.Dispose();

            Core.Logger.Debug($"Connection Disposed. Active connection left: " + Core.TcpClientCollection.Count);
        }
    }
}
