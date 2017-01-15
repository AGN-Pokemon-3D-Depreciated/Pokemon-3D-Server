using Amib.Threading;
using Modules.System.Threading;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Pokemon_3D_Server_Core.Server.Game.Server
{
    public class Networking : IDisposable
    {
        public TcpClient TcpClient { get; private set; }
        public Player.Player Player { get; set; }
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
            Writer = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true };

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
                                    Dispose();
                            }
                        }
                        else
                            Dispose();
                    }
                    catch (ThreadAbortException) { return; }
                    catch (Exception)
                    {
                        if (IsActive)
                            Dispose();
                    }
                } while (IsActive);
            });
        }

        public void SentToPlayer(Package.Package p)
        {
            ThreadPool2.QueueWorkItem(() =>
            {
                try
                {
                    Writer.WriteLine(p.ToString());
                    Core.Logger.Debug($"Sent: {p.ToString()}", this);
                }
                catch (Exception) { }
            });
        }

        public void Dispose()
        {
            IsActive = false;
            ThreadPool2.WaitForIdle();

            if (Player != null)
                Player.Dispose();

            Core.TcpClientCollection.Remove(TcpClient);

            if (TcpClient != null) TcpClient.Close();
            if (Reader != null) Reader.Dispose();
            if (Writer != null) Writer.Dispose();

            Thread.Dispose();
        }
    }
}
