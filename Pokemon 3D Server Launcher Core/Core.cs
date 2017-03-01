using Modules.System;
using Modules.System.IO;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Logger;
using Pokemon_3D_Server_Launcher_Core.Interfaces.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pokemon_3D_Server_Launcher_Core
{
    public sealed class Core : ICore
    {
        public string ModuleName { get; } = "Core";
        public Version ModuleVersion { get; } = new Version(0, 54, 1, 0);

        public ISettings Settings { get; set; }
        public ILogger Logger { get; }

        public ICore BaseInstance { get; private set; }

        private Settings.Settings SettingsInstance;
        private Logger.Logger LoggerInstance;

        private bool Stopping = false;

        public Core()
        {
            ExceptionHelper.Core = this;
            SettingsInstance = new Settings.Settings(this);
            Settings = SettingsInstance;
            LoggerInstance = new Logger.Logger(this);
            Logger = LoggerInstance;
        }

        public void Start(ICore instance)
        {
            BaseInstance = instance;

            foreach (string dll in SettingsInstance.ModulesToLoad)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom($"{SettingsInstance.Directories.ModulesDirectory}/{dll}".GetFullPath());
                    List<TypeInfo> classImplmeneted = assembly.DefinedTypes.ToList();

                    foreach (TypeInfo @class in classImplmeneted)
                    {
                        if (@class.ImplementedInterfaces.Where(a => a.FullName == typeof(ICore).FullName).Count() > 0)
                        {
                            ICore newInstance = assembly.CreateInstance(@class.FullName) as ICore;
                            newInstance.Start(this);
                            newInstance.Logger.OnLogMessageReceived += (sender, e) => LoggerInstance.EventLog(sender, e);
                            Logger.Log($"{newInstance.ModuleName} v{newInstance.ModuleVersion} have loaded.", "Info");
                            SettingsInstance.LoadedInstances.Add(newInstance);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.CatchError();
                }
            }
        }

        public void Stop(int exitCode)
        {
            if (!Stopping)
            {
                Stopping = true;

                foreach (ICore loadedInstances in SettingsInstance.LoadedInstances)
                    loadedInstances.Stop(exitCode);

                Environment.Exit(exitCode);
            }
        }

        public void Invoke(string method, object[] param)
        {
        }
    }
}