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
        /// <summary>
        /// Get the name of the module.
        /// </summary>
        [YamlIgnore]
        public string Name { get { return "Application Settings"; } }

        /// <summary>
        /// Get the version of the module.
        /// </summary>
        [YamlIgnore]
        public string Version { get { return "0.54"; } }

        /// <summary>
        /// Get Directories.
        /// </summary>
        [YamlIgnore]
        public Directories Directories { get; private set; } = new Directories();

        /// <summary>
        /// Get Logger.
        /// </summary>
        public Logger Logger { get; private set; } = new Logger();

        /// <summary>
        /// Get Server.
        /// </summary>
        public Server.Server Server { get; private set; } = new Server.Server();

        private string SettingPath;

        public Settings()
        {
            SettingPath = $"{Directories.ApplicationDirectory}/ApplicationSetting.yml".GetFullPath();
        }

        /// <summary>
        /// Start the module.
        /// </summary>
        public void Start()
        {
            if (File.Exists(SettingPath))
            {
                Exception ex;
                Settings NewSettings = DeserializerHelper.Deserialize<Settings>(SettingPath, out ex);

                if (ex == null && NewSettings != null)
                {
                    Core.Settings = NewSettings;
                    Core.Logger.Log("Application Settings Loaded.");
                }
                else
                    ex.CatchError();
            }
        }

        /// <summary>
        /// Stop the module.
        /// </summary>
        public void Stop()
        {
            Exception ex;
            Core.Settings.Serialize(SettingPath, out ex);

            if (ex == null)
                Core.Logger.Log("Application Settings Saved.");
            else
                ex.CatchError();
        }
    }
}
