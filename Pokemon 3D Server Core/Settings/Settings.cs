using Modules.System;
using Modules.System.IO;
using Modules.YamlDotNet.Serialization;
using Pokemon_3D_Server_Core.Interface;
using System;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Core.Settings
{
    public class Settings : IModules
    {
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

        /// <summary>
        /// Get the name of the module.
        /// </summary>
        [YamlIgnore]
        public string Name { get { return "Settings"; } }

        /// <summary>
        /// Get the version of the module.
        /// </summary>
        [YamlIgnore]
        public string Version { get { return "0.54"; } }

        /// <summary>
        /// Start the module.
        /// </summary>
        public void Start()
        {
            Exception ex;
            Settings NewSettings = DeserializerHelper.Deserialize<Settings>($"{Directories.ApplicationDirectory}/ApplicationSetting.yml".GetFullPath(), out ex);

            if (ex == null && NewSettings != null)
            {
                Core.Settings = NewSettings;
                IModules OldSettings = Core.ActiveModules.Find(a => a.Name == Name && a.Version == Version);
                OldSettings = NewSettings;
                Core.Logger.Log("Settings loaded.");
            }
            else if (ex != null)
                ex.CatchError();
        }

        /// <summary>
        /// Stop the module.
        /// </summary>
        public void Stop()
        {
            Exception ex;
            Core.Settings.Serialize($"{Directories.ApplicationDirectory}/ApplicationSetting.yml".GetFullPath(), out ex);

            if (ex == null)
                Core.Logger.Log("Settings saved.");
            else
                ex.CatchError();
        }
    }
}
