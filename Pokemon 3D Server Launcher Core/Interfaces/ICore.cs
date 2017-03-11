using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;

namespace Pokemon_3D_Server_Launcher_Core.Interfaces
{
    public interface ICore
    {
        string ModuleName { get; }
        Version ModuleVersion { get; }

        ISettings Settings { get; }
        Core BaseInstance { get; }

        void Start(Core instance);

        void Stop(int exitCode);

        object Invoke(string method, params object[] param);
    }
}