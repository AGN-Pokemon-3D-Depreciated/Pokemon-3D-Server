using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;

namespace Pokemon_3D_Server_Launcher_Game_Module
{
    public class Core : ICore
    {
        public string ModuleName { get; } = "Game";
        public Version ModuleVersion { get; } = new Version(0, 54, 1, 0);

        public ISettings Settings { get; internal set; }

        public Settings.Settings InternalSettings { get; internal set; }
        public Logger.Logger Logger { get; private set; }

        public Pokemon_3D_Server_Launcher_Core.Core BaseInstance { get; private set; }

        public void Start(Pokemon_3D_Server_Launcher_Core.Core instance)
        {
            BaseInstance = instance;
            Settings = new Settings.Settings(this);
            Logger = new Logger.Logger(this);
        }

        public void Stop(int exitCode)
        {
            InternalSettings.Save();
        }

        public object Invoke(string method, object[] param)
        {
            return null;
        }
    }
}