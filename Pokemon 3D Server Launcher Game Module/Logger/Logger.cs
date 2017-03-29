using Pokemon_3D_Server_Launcher_Game_Module.Server;

namespace Pokemon_3D_Server_Launcher_Game_Module.Logger
{
    public class Logger
    {
        private Core Core;

        public Logger(Core core)
        {
            Core = core;
        }

        public void Log(string message, string type = "Info", Networking network = null, bool printToConsole = true, bool writeToLog = true)
        {
            if (network != null)
            {
                string publicAddress = network.GetPublicIPFromClient();

                if (publicAddress != null)
                    Core.BaseInstance.Logger.Log(Core.BaseCore, $"{publicAddress} {message}", type, printToConsole, writeToLog);
                else
                    Core.BaseInstance.Logger.Log(Core.BaseCore, message, type, printToConsole, writeToLog);
            }
            else
                Core.BaseInstance.Logger.Log(Core.BaseCore, message, type, printToConsole, writeToLog);
        }

        public void Debug(string message, Networking network = null, bool printToConsole = true, bool writeToLog = true)
        {
            if (network != null)
            {
                string publicAddress = network.GetPublicIPFromClient();

                if (publicAddress != null)
                    Core.BaseInstance.Logger.Log(Core.BaseCore, $"{publicAddress} {message}", "Debug", printToConsole, writeToLog);
                else
                    Core.BaseInstance.Logger.Log(Core.BaseCore, message, "Debug", printToConsole, writeToLog);
            }
            else
                Core.BaseInstance.Logger.Log(Core.BaseCore, message, "Debug", printToConsole, writeToLog);
        }
    }
}