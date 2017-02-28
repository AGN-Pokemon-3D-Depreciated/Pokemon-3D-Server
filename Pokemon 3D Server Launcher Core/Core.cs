using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Logger;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;

namespace Pokemon_3D_Server_Launcher_Core
{
    public sealed class Core : ICore
    {
        public string ModuleName { get; } = "Core";
        public Version ModuleVersion { get; } = new Version(0, 54, 1, 0);

        public ILogger Logger { get; }
        public ISettings Settings { get; set; }

        public Core()
        {
            Settings = new Settings.Settings(this);
            Logger = new Logger.Logger(this);
        }

        public void Start(ICore instance)
        {

        }

        public void Stop(int exitCode)
        {
            if (exitCode == 0)
                Environment.Exit(0);
        }

        public void Invoke(string method, object[] param)
        {

        }
    }
}