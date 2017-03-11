using System;

namespace Pokemon_3D_Server_Launcher_Core.Logger
{
    public sealed class LoggerEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public LoggerEventArgs(string message)
        {
            Message = message;
        }
    }
}