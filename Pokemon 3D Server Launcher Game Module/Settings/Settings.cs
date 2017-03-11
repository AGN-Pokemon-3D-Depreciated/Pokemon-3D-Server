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

        public Server.Server Server { get; private set; } = new Server.Server();
        public Tokens.Tokens Tokens { get; private set; } = new Tokens.Tokens();
        public World.World World { get; private set; } = new World.World();
        public Features.Features Features { get; set; } = new Features.Features();

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
                Core.Settings = DeserializerHelper.Deserialize<Settings>($"{Core.BaseInstance.Settings.Directories.DataDirectory}/GameServerSetting.yml".GetFullPath()) ?? new Settings();
                Core.Settings.Core = Core;
            }
            else
                Save();
        }

        public override void Save()
        {
            Core.Settings.Serialize($"{Core.BaseInstance.Settings.Directories.DataDirectory}/GameServerSetting.yml".GetFullPath());
        }
    }
}