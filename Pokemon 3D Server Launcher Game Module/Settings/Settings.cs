using Modules.System.IO;
using Modules.YamlDotNet.Serialization;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using Pokemon_3D_Server_Launcher_Core.Settings;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Launcher_Game_Module.Settings
{
    public class Settings : BaseSettings
    {
        [YamlIgnore]
        public override Dictionary<string, bool> LogTypes { get; set; }

        private string DataDirectory;

        public Settings()
        {
        }

        public Settings(ICore core, bool load = true) : base(core)
        {
            DataDirectory = Core.BaseInstance.Settings.GetSettings<string>("Directories.DataDirectory");
            LogTypes = new Dictionary<string, bool>() { { "Info", true }, { "Warning", true }, { "Error", true }, { "Debug", false } };

            if (load == true)
                Load();
        }

        public Settings(ICore core, ISettings settings) : base(core)
        {
            if (settings == null)
                Core.Settings = new Settings(Core, false);
            else
                Core.Settings = settings;
        }

        public override void Load()
        {
            if (File.Exists($"{DataDirectory}/GameServerSetting.yml".GetFullPath()))
                Core.Settings = new Settings(Core, DeserializerHelper.Deserialize<Settings>($"{DataDirectory}/GameServerSetting.yml".GetFullPath()));
            else
                Save();
        }

        public override void Save()
        {
            this.Serialize($"{DataDirectory}/GameServerSetting.yml".GetFullPath());
        }
    }
}
