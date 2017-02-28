using System;

namespace Pokemon_3D_Server_Launcher_Core.Logger
{
    public sealed class LoggerEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public bool PrintToConsole { get; private set; }
        public bool WriteToLog { get; private set; }

        public LoggerEventArgs(string message, bool printToConsole = true, bool writeToLog = true)
        {
            Message = message;
            PrintToConsole = printToConsole;
            WriteToLog = writeToLog;
        }
    }
}
