using System.Collections.Generic;

namespace Pokemon_3D_Server_Launcher_Core.Interfaces.Settings
{
    public interface ISettings
    {
        Dictionary<string, bool> LogTypes { get; }

        void Load();

        void Save();

        T GetSettings<T>(string setting);

        void SetSettings(string setting, object value);
    }
}