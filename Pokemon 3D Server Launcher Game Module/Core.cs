using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Logger;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;

namespace Pokemon_3D_Server_Launcher_Game_Module
{
    public class Core : ICore
    {
        public string ModuleName { get; } = "Game";
        public Version ModuleVersion { get; } = new Version(0, 54, 1, 0);

        public ILogger Logger { get; private set; }
        public ISettings Settings { get; set; }

        public ICore BaseInstance { get; private set; }

        public Core()
        {
        }

        public void Start(ICore instance)
        {
            BaseInstance = instance;
            Settings = new Settings.Settings(this);
            Logger = new Logger.Logger(this);
        }

        public void Stop(int exitCode)
        {
        }

        public void Invoke(string method, object[] param)
        {
        }
    }
}