using Modules.System;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Logger;
using System;
using System.Threading.Tasks;

namespace Pokemon_3D_Server_Launcher_Game_Module.Logger
{
    public class Logger : BaseLogger
    {
        public Logger(ICore core) : base(core)
        {
        }

        public override event EventHandler<LoggerEventArgs> OnLogMessageReceived;

        public override void Log(string message, string type, bool printToConsole = true, bool writeToLog = true)
        {
            Task.Run(() =>
            {
                if (CanLog(type))
                    OnLogMessageReceived.Invoke(this, new LoggerEventArgs($"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")} [{Core.ModuleName}|{type}] {message}", printToConsole, writeToLog));
            }).ContinueWith(task => task.Exception?.CatchError());
        }
    }
}