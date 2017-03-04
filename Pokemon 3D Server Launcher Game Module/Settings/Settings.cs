using Modules.System.IO;
using Modules.YamlDotNet.Serialization;
using Pokemon_3D_Server_Launcher_Core.Settings;
using System.Collections.Generic;
using System.IO;

namespace Pokemon_3D_Server_Launcher_Game_Module.Settings
{
    public class Settings : BaseSettings
    {
        private Core Core;

        public override Dictionary<string, bool> LogTypes { get; protected set; } = new Dictionary<string, bool>() { { "Info", true }, { "Warning", true }, { "Error", true }, { "Debug", false } };

        public Network.Network Network { get; private set; } = new Network.Network();

        public Settings()
        {
        }

        public Settings(Core core)
        {
            Core = core;
            Load();
        }

        public override void Load()
        {
            if (File.Exists($"{Core.BaseInstance.Settings.Directories.DataDirectory}/GameServerSetting.yml".GetFullPath()))
            {
                Core.InternalSettings = DeserializerHelper.Deserialize<Settings>($"{Core.BaseInstance.Settings.Directories.DataDirectory}/GameServerSetting.yml".GetFullPath());
                Core.InternalSettings.Core = Core;
                Core.Settings = Core.InternalSettings;
            }
            else
                Save();
        }

        public override void Save()
        {
            this.Serialize($"{Core.BaseInstance.Settings.Directories.DataDirectory}/GameServerSetting.yml".GetFullPath());
        }
    }
}
