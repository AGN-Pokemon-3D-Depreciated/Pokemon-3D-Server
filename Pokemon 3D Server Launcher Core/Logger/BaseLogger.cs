using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Logger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokemon_3D_Server_Launcher_Core.Logger
{
    public abstract class BaseLogger : ILogger
    {
        protected ICore Core;

        public abstract event EventHandler<LoggerEventArgs> OnLogMessageReceived;

        protected BaseLogger(ICore core)
        {
            Core = core;
        }

        public abstract void Log(string message, string type, bool printToConsole = true, bool writeToLog = true);

        public virtual bool CanLog(string type)
        {
            Dictionary<string, bool> LogTypes = Core.Settings.GetSettings<Dictionary<string, bool>>("LogTypes");

            if (LogTypes != null)
            {
                if (LogTypes.ContainsKey(type))
                    return LogTypes[type];
            }

            return false;
        }
    }
}