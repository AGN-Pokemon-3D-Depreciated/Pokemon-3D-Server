using Modules.System.IO;
using Modules.YamlDotNet.Serialization;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Launcher_Core.Settings
{
    public sealed class Settings
    {
        private Core Core;

        [YamlIgnore]
        public Application.Application Application { get; private set; } = new Application.Application();

        [YamlIgnore]
        public Directories.Directories Directories { get; private set; } = new Directories.Directories();

        public Dictionary<string, bool> LogTypes { get; set; }

        public List<string> ModulesToLoad { get; private set; } = new List<string>();

        internal Settings()
        {
        }

        internal Settings(Core core)
        {
            LogTypes = new Dictionary<string, bool>() { { "Info", true }, { "Warning", true }, { "Error", true }, { "Debug", false } };

            foreach (string module in Directory.GetFiles(Directories.ModulesDirectory, "*.dll", SearchOption.AllDirectories))
            {
                Assembly assembly = Assembly.LoadFrom(module);

                if (assembly.DefinedTypes.Select(a => a.ImplementedInterfaces).Where(b => b.Where(c => c.FullName == typeof(ICore).FullName).Count() > 0).Count() > 0)
                    ModulesToLoad.Add(module.Replace(Directories.ModulesDirectory.NormalizeFilePath(), "").TrimStart('/', '\\'));
            }

            Load();
        }

        internal void Load()
        {
            if (File.Exists($"{Directories.DataDirectory}/ApplicationSetting.yml".GetFullPath()))
            {
                Core.Settings = DeserializerHelper.Deserialize<Settings>($"{Directories.DataDirectory}/ApplicationSetting.yml".GetFullPath()) ?? new Settings();
                Core.Settings.Core = Core;
            }
            else
            {
                Save();
                Core.Stop(0);
            }
        }

        internal void Save()
        {
            this.Serialize($"{Directories.DataDirectory}/ApplicationSetting.yml".GetFullPath());
            Core.Logger.Log("Settings Saved", "Info");
        }
    }
}