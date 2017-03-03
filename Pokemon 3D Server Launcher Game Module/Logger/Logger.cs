namespace Pokemon_3D_Server_Launcher_Game_Module.Logger
{
    public class Logger
    {
        private Core Core;

        public Logger(Core core)
        {
            Core = core;
        }

        public void Log(string message, string type, bool printToConsole = true, bool writeToLog = true)
        {
            Core.BaseInstance.Logger.Log(Core, message, type, printToConsole, writeToLog);
        }
    }
}