using Pokemon_3D_Server_Launcher_Core.Interfaces.Logger;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;

namespace Pokemon_3D_Server_Launcher_Core.Interfaces
{
    public interface ICore
    {
        string ModuleName { get; }
        Version ModuleVersion { get; }

        ISettings Settings { get; set; }
        ILogger Logger { get; }

        ICore BaseInstance { get; }

        void Start(ICore instance);

        void Stop(int exitCode);

        void Invoke(string method, object[] param);
    }
}
