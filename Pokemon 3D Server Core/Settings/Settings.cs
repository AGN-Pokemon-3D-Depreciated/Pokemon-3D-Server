using Modules.System;
using Modules.System.IO;
using Modules.YamlDotNet.Serialization;
using Pokemon_3D_Server_Core.Interface;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Core.Settings
{
    public class Settings : IModules
    {
        [YamlIgnore]
        public string Name { get; } = "Application Settings";

        [YamlIgnore]
        public string Version { get; } = "0.54";

        [YamlIgnore]
        public Directories.Directories Directories { get; } = new Directories.Directories();

        public Logger.Logger Logger { get; private set; } = new Logger.Logger();
        public Server.Server Server { get; private set; } = new Server.Server();

        public void Start()
        {
            if (Load())
                Core.Logger.Log("Application Settings Loaded.");
        }

        public void Refresh()
        {
            if (Load())
                Core.Logger.Log("Application Settings Refreshed.");
        }

        public void Stop()
        {
            if (Save())
                Core.Logger.Log("Application Settings Saved.");
        }

        private bool Load()
        {
            if (File.Exists($"{Directories.ApplicationDirectory}/ApplicationSetting.yml".GetFullPath()))
            {
                Exception ex;
                Settings newSettings = DeserializerHelper.Deserialize<Settings>($"{Directories.ApplicationDirectory}/ApplicationSetting.yml".GetFullPath(), out ex);

                if (ex == null && newSettings != null)
                {
                    Core.Settings = newSettings;
                    return true;
                }
                else
                {
                    ex.CatchError();
                    return false;
                }
            }
            else
                return true;
        }

        private bool Save()
        {
            Exception ex;
            Core.Settings.Serialize($"{Directories.ApplicationDirectory}/ApplicationSetting.yml".GetFullPath(), out ex);

            if (ex == null)
                return true;
            else
            {
                ex.CatchError();
                return false;
            }
        }
    }
}