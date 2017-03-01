using Modules.System;
using Modules.System.IO;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using System;
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
        }

        public override void Log(string message, string type, bool printToConsole = true, bool writeToLog = true)
        {
            Task.Run(() =>
            {
                if (CanLog(type))
                    InternalLog($"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")} [{Core.ModuleName}|{type}] {message}", printToConsole, writeToLog);
            }).ContinueWith(task => task.Exception?.CatchError());
        }

        public void EventLog(object sender, LoggerEventArgs e)
        {
            Task.Run(() =>
            {
                InternalLog(e.Message, e.PrintToConsole, e.WriteToLog);
            }).ContinueWith(task => task.Exception?.CatchError());
        }

        private void InternalLog(string message, bool printToConsole = true, bool writeToLog = true)
        {
            if (IsActive)
            {
                if (printToConsole)
                    OnLogMessageReceived.BeginInvoke(this, new LoggerEventArgs(message), null, null);

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
    }
}