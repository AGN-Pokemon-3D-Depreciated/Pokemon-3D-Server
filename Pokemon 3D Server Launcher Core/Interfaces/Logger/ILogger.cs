using Pokemon_3D_Server_Launcher_Core.Logger;
using System;

namespace Pokemon_3D_Server_Launcher_Core.Interfaces.Logger
{
    public interface ILogger
    {
        event EventHandler<LoggerEventArgs> OnLogMessageReceived;

        void Log(string message, string type, bool printToConsole = true, bool writeToLog = true);

        void Debug(string message, bool printToConsole = true, bool writeToLog = true);

        bool CanLog(string type);
    }
}
