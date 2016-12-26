using Amib.Threading;
using Modules.System.Threading;
using System;
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
        /// Get/Set Force remove session.
        /// </summary>
        public bool ForceRemove { get; set; } = false;

        private StreamReader Reader;
        private StreamWriter Writer;

        private bool IsActive = false;

        private ThreadHelper Thread = new ThreadHelper();

        private IWorkItemsGroup ThreadPool = new SmartThreadPool().CreateWorkItemsGroup(Environment.ProcessorCount);
        private IWorkItemsGroup ThreadPool2 = new SmartThreadPool().CreateWorkItemsGroup(1);

        public Networking(TcpClient TcpClient)
        {
            this.TcpClient = TcpClient;

            Reader = new StreamReader(TcpClient.GetStream());
            Writer = new StreamWriter(TcpClient.GetStream());

            IsActive = true;

            Thread.Add(() =>
            {
                int ErrorCount = 0;

                do
                {
                    try
                    {
                        if (TcpClient.Available > -1)
                        {
                            string PackageString = Reader.ReadLine();

                            if (!string.IsNullOrWhiteSpace(PackageString))
                            {
                                ThreadPool.QueueWorkItem(() =>
                                {
                                    Package.Package Package = new Package.Package(PackageString, TcpClient);

                                    if (Package.IsValid)
                                        Package.Handle();

                                    Core.Logger.Debug($"Receive: {PackageString}", TcpClient);
                                });

                                ErrorCount = 0;
                            }
                            else
                            {
                                ErrorCount++;

                                if (ErrorCount > 10)
                                {
                                    Core.Logger.Debug("Too much error received from the client.", TcpClient);
                                    throw new Exception("Too much error received from the client.");
                                }
                            }
                        }
                        else
                        {
                            Core.Logger.Debug("Unexpected error occured.", TcpClient);
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
                    Core.Logger.Debug($"Sent: {p.ToString()}", TcpClient);
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

            if (TcpClient != null) TcpClient.Close();

            if (Reader != null) Reader.Dispose();
            if (Writer != null) Writer.Dispose();

            Thread.Dispose();
            if (!ForceRemove) Core.TcpClientCollection.Remove(TcpClient);
        }
    }
}
