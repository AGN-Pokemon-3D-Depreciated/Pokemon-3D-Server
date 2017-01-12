using Amib.Threading;
using Modules.System.IO;
using Modules.System.Net;
using Pokemon_3D_Server_Core.Interface;
using Pokemon_3D_Server_Core.Server.Game.Server;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using static Pokemon_3D_Server_Core.Collections.LogTypeCollection;

namespace Pokemon_3D_Server_Core.Logger
{
    public class Logger : IModules
    {
        public string Name { get; } = "Application Logger";
        public string Version { get; } = "0.54";
        public ILogger instance { get; set; }

        private FileStream FileStream;
        private StreamWriter Writer;
        private IWorkItemsGroup ThreadPool = new SmartThreadPool().CreateWorkItemsGroup(1);
        private bool IsActive = false;

        public Logger()
        {
            InitLogger();
        }

        public void Start()
        {
            InitLogger();
        }

        public void Stop()
        {
            IsActive = false;
            ThreadPool.WaitForIdle();
            Writer.Dispose();
            FileStream.Dispose();
        }

        private void InitLogger()
        {
            FileStream = new FileStream($"{Core.Settings.Directories.LoggerDirectory}/Logger_{DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss")}.dat".GetFullPath(), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            Writer = new StreamWriter(FileStream, Encoding.UTF8) { AutoFlush = true };
            IsActive = true;
        }

        public void Log(string message, LogTypes logType = LogTypes.Info, Networking network = null, bool printToConsole = true, bool writeToLog = true)
        {
            if (IsActive)
            {
                ThreadPool.QueueWorkItem(() =>
                {
                    if (CanLog(logType))
                    {
                        string messageToLog = $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + (network == null ? "" : " " + network.GetPublicIPFromClient())} [{logType.ToString()}] {message}";

                        if (printToConsole && instance != null)
                            instance.LogMessage(messageToLog);

                        if (writeToLog)
                            Writer.WriteLine(messageToLog);
                    }
                });
            }
        }

        public void Debug(string message, Networking network = null, bool printToConsole = true, bool writeToLog = true)
        {
            if (IsActive)
            {
                ThreadPool.QueueWorkItem(() =>
                {
                    if (CanLog(LogTypes.Debug))
                    {
                        string messageToLog = $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + (network == null ? "" : " " + network.GetPublicIPFromClient())} [{LogTypes.Debug.ToString()}] {message}";

                        if (printToConsole && instance != null)
                            instance.LogMessage(messageToLog);

                        if (writeToLog)
                            Writer.WriteLine(messageToLog);
                    }
                });
            }
        }

        private bool CanLog(LogTypes logType)
        {
            if (logType == LogTypes.Info && Core.Settings.Logger.LoggerInfo)
                return true;
            else if (logType == LogTypes.Warning && Core.Settings.Logger.LoggerWarning)
                return true;
            else if (logType == LogTypes.Error && Core.Settings.Logger.LoggerError)
                return true;
            else if ((logType == LogTypes.Debug && Core.Settings.Logger.LoggerDebug) || Debugger.IsAttached)
                return true;
            else if (logType == LogTypes.Chat && Core.Settings.Server.Game.Logger.LoggerChat)
                return true;
            else if (logType == LogTypes.PM && Core.Settings.Server.Game.Logger.LoggerPM)
                return true;
            else if (logType == LogTypes.Server && Core.Settings.Server.Game.Logger.LoggerServer)
                return true;
            else if (logType == LogTypes.Trade && Core.Settings.Server.Game.Logger.LoggerTrade)
                return true;
            else if (logType == LogTypes.PvP && Core.Settings.Server.Game.Logger.LoggerPvP)
                return true;
            else if (logType == LogTypes.Command && Core.Settings.Server.Game.Logger.LoggerCommand)
                return true;
            else
                return false;
        }
    }
}
