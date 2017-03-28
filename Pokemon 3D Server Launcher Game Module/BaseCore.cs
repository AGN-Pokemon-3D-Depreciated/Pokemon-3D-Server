using Modules.System;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;

namespace Pokemon_3D_Server_Launcher_Game_Module
{
    public class BaseCore : ICore
    {
        public string ModuleName { get; } = "Game";
        public Version ModuleVersion { get; } = new Version(0, 54, 1, 0);

        public ISettings Settings { get { return Core.Settings; } }

        public Pokemon_3D_Server_Launcher_Core.Core BaseInstance { get; private set; }

        private Core Core;

        public void Start(Pokemon_3D_Server_Launcher_Core.Core instance)
        {
            BaseInstance = instance;
            AppDomain.CurrentDomain.UnhandledException += (sender, ex) => { ((Exception)ex.ExceptionObject).CatchError(); };
            Core = new Core(this);
            Core.Start();
        }

        public void Stop(int exitCode)
        {
            Core.Stop(exitCode);
        }

        public object Invoke(string method, params object[] param)
        {
            return null;
        }
    }
}
