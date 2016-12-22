using Amib.Threading;
using Modules.System.IO;
using Pokemon_3D_Server_Core.Interface;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static Pokemon_3D_Server_Core.Settings.Logger;

namespace Pokemon_3D_Server_Core.Logger
{
    public class Logger : IModules
    {
        private FileStream FileStream;
        private StreamWriter Writer;

        private ILogger instance;

        private bool IsActive = false;
        private IWorkItemsGroup ThreadPool = new SmartThreadPool().CreateWorkItemsGroup(1);

        /// <summary>
        /// Get the name of the module.
        /// </summary>
        public string Name { get { return "Logger"; } }

        /// <summary>
        /// Get the version of the module.
        /// </summary>
        public string Version { get { return "0.54"; } }

        public Logger()
        {
            FileStream = new FileStream($"{Core.Settings.Directories.LoggerDirectory}/Logger_{DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss")}.dat".GetFullPath(), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            Writer = new StreamWriter(FileStream, Encoding.UTF8);
            IsActive = true;
        }

        /// <summary>
        /// Start the module.
        /// </summary>
        public void Start() { }

        /// <summary>
        /// Stop the module.
        /// </summary>
        public void Stop()
        {
            IsActive = false;
            ThreadPool.WaitForIdle();

            Writer.Dispose();
            FileStream.Dispose();
        }

        /// <summary>
        /// Register Logger.
        /// </summary>
        /// <param name="module"></param>
        public void RegisterLog(ILogger module)
        {
            instance = module;
        }

        /// <summary>
        /// Log server message.
        /// </summary>
        /// <param name="Message">Message to log.</param>
        /// <param name="LogType">Log type.</param>
        /// <param name="TcpClient">TcpClient.</param>
        /// <param name="Console">Print to console?</param>
        /// <param name="WriteToLog">Write to log file?</param>
        public void Log(string Message, LogTypes LogType = LogTypes.Info, TcpClient TcpClient = null, bool Console = true, bool WriteToLog = true)
        {
            if (IsActive)
            {
                ThreadPool.QueueWorkItem(() =>
                {
                    if (CanLog(LogType))
                    {
                        string MessageToLog = $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + (TcpClient == null ? "" : " " + ((IPEndPoint)TcpClient.Client.RemoteEndPoint).Address.ToString())} [{LogType.ToString()}] {Message}";

                        if (Console && instance != null)
                            instance.LogMessage(MessageToLog);

                        if (WriteToLog)
                        {
                            Writer.WriteLine(MessageToLog);
                            Writer.Flush();
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Log debug message.
        /// </summary>
        /// <param name="Message">Message to log.</param>
        /// <param name="TcpClient">TcpClient.</param>
        /// <param name="Console">Print to console?</param>
        /// <param name="WriteToLog">Write to log file?</param>
        public void Debug(string Message, TcpClient TcpClient = null, bool Console = true, bool WriteToLog = true)
        {
            if (IsActive)
            {
                ThreadPool.QueueWorkItem(() =>
                {
                    if (CanLog(LogTypes.Debug))
                    {
                        string MessageToLog = $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + (TcpClient == null ? "" : " " + ((IPEndPoint)TcpClient.Client.RemoteEndPoint).Address.ToString())} [{LogTypes.Debug.ToString()}] {Message}";

                        if (Console && instance != null)
                            instance.LogMessage(MessageToLog);

                        if (WriteToLog)
                        {
                            Writer.WriteLine(MessageToLog);
                            Writer.Flush();
                        }
                    }
                });
            }
        }

        private bool CanLog(LogTypes LogType)
        {
            if (LogType == LogTypes.Chat && Core.Settings.Logger.LoggerChat)
                return true;
            else if (LogType == LogTypes.Command && Core.Settings.Logger.LoggerCommand)
                return true;
            else if ((LogType == LogTypes.Debug && Core.Settings.Logger.LoggerDebug) || Debugger.IsAttached)
                return true;
            else if (LogType == LogTypes.Info && Core.Settings.Logger.LoggerInfo)
                return true;
            else if (LogType == LogTypes.PM && Core.Settings.Logger.LoggerPM)
                return true;
            else if (LogType == LogTypes.PvP && Core.Settings.Logger.LoggerPvP)
                return true;
            else if (LogType == LogTypes.Server && Core.Settings.Logger.LoggerServer)
                return true;
            else if (LogType == LogTypes.Trade && Core.Settings.Logger.LoggerTrade)
                return true;
            else if (LogType == LogTypes.Warning && Core.Settings.Logger.LoggerWarning)
                return true;
            else if (LogType == LogTypes.Error && Core.Settings.Logger.LoggerError)
                return true;
            else if (LogType == LogTypes.Rcon && Core.Settings.Logger.LoggerRcon)
                return true;
            else
                return false;
        }
    }
}
