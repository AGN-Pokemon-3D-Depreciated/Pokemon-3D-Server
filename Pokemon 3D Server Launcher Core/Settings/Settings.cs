using Modules.System.IO;
using Modules.YamlDotNet.Serialization;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Launcher_Core.Settings
{
    internal class Settings : BaseSettings
    {
        [YamlIgnore]
        public Application.Application Application { get; private set; } = new Application.Application();

        [YamlIgnore]
        public Directories.Directories Directories { get; private set; } = new Directories.Directories();

        public override Dictionary<string, bool> LogTypes { get; set; }

        public List<string> ModulesToLoad { get; private set; } = new List<string>();

        [YamlIgnore]
        public List<ICore> LoadedInstances { get; private set; } = new List<ICore>();

        public Settings()
        {
        }

        public Settings(ICore core, bool load = true) : base(core)
        {
            LogTypes = new Dictionary<string, bool>() { { "Info", true }, { "Warning", true }, { "Error", true }, { "Debug", false } };

            foreach (string module in Directory.GetFiles(Directories.ModulesDirectory, "*.dll", SearchOption.AllDirectories))
            {
                Assembly assembly = Assembly.LoadFrom(module);

                if (assembly.DefinedTypes.Select(a => a.ImplementedInterfaces).Where(b => b.Where(c => c.FullName == typeof(ICore).FullName).Count() > 0).Count() > 0)
                    ModulesToLoad.Add(module.Replace(Directories.ModulesDirectory.NormalizeFilePath(), "").TrimStart('/', '\\'));
            }

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
            if (File.Exists($"{Directories.DataDirectory}/ApplicationSetting.yml".GetFullPath()))
                Core.Settings = new Settings(Core, DeserializerHelper.Deserialize<Settings>($"{Directories.DataDirectory}/ApplicationSetting.yml".GetFullPath()));
            else
            {
                Save();
                Core.Stop(0);
            }
        }

        public override void Save()
        {
            this.Serialize($"{Directories.DataDirectory}/ApplicationSetting.yml".GetFullPath());
        }
    }
}