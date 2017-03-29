using Pokemon_3D_Server_Launcher_Core;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;

namespace Pokemon_3D_Server_Launcher_Game_Module
{
    public class BaseCore : AbstractCore
    {
        public override string ModuleName { get; } = "Game";
        public override Version ModuleVersion { get; } = new Version(0, 54, 1, 0);

        private Core Core;

        public override ISettings Settings { get { return Core.Settings; } }

        public override void Start(Pokemon_3D_Server_Launcher_Core.Core instance)
        {
            base.Start(instance);
            Core = new Core(this);
            Core.Start();
        }

        public override void Stop(int exitCode)
        {
            Core.Stop(exitCode);
        }

        public override object Invoke(string method, params object[] param)
        {
            return null;
        }
    }
}