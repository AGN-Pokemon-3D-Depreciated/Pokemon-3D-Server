using Modules.System;
using Modules.System.IO;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_3D_Server_Launcher_Core.Logger
{
    internal class Logger : BaseLogger
    {
        private StreamWriter Writer;
        private bool IsActive = false;
        private object WriterLock = new object();

        public override event EventHandler<LoggerEventArgs> OnLogMessageReceived;

        public Logger(ICore core) : base(core)
        {
            Writer = new StreamWriter(new FileStream($"{Core.Settings.GetSettings<string>("Directories.LoggerDirectory")}/Logger_{DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss")}.dat".GetFullPath(), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite), Encoding.UTF8);
            IsActive = true;

            List<ICore> LoadedInstances = Core.Settings.GetSettings<List<ICore>>("LoadedInstances");

            foreach (ICore Instance in LoadedInstances)
                Instance.Logger.OnLogMessageReceived += (sender, e) => EventLog(sender, e);
        }

        public override void Log(string message, string type, bool printToConsole = true, bool writeToLog = true)
        {
            Task.Run(() =>
            {
                if (CanLog(type))
                    InternalLog($"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")} [{type}] {message}", printToConsole, writeToLog);
            }).ContinueWith(task => task.Exception?.CatchError());
        }

        public override void Debug(string message, bool printToConsole = true, bool writeToLog = true)
        {
            Task.Run(() =>
            {
                if (CanLog("Debug"))
                    InternalLog($"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")} [Debug] {message}", printToConsole, writeToLog);
            }).ContinueWith(task => task.Exception?.CatchError());
        }

        public void EventLog(object sender, LoggerEventArgs e)
        {
            InternalLog(e.Message, e.PrintToConsole, e.WriteToLog);
        }

        public void InternalLog(string message, bool printToConsole = true, bool writeToLog = true)
        {
            try
            {
                if (IsActive)
                {
                    if (printToConsole)
                        OnLogMessageReceived.Invoke(this, new LoggerEventArgs(message));

                    if (writeToLog)
                    {
                        lock (WriterLock)
                        {
                            Writer?.WriteLine(message);
                            Writer?.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.CatchError();
            }
        }
    }
}