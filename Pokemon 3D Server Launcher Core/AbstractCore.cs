using Modules.System;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;

namespace Pokemon_3D_Server_Launcher_Core
{
    public abstract class AbstractCore : ICore
    {
        public abstract string ModuleName { get; }
        public abstract Version ModuleVersion { get; }

        public abstract ISettings Settings { get; }

        public Core BaseInstance { get; private set; }

        public virtual void Start(Core instance)
        {
            BaseInstance = instance;
            AppDomain.CurrentDomain.UnhandledException += (sender, ex) => { ((Exception)ex.ExceptionObject).CatchError(); };
        }

        public virtual void Stop(int exitCode)
        {
        }

        public virtual object Invoke(string method, params object[] param)
        {
            return null;
        }
    }
}