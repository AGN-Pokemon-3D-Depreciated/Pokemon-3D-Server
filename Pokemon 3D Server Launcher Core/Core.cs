using Modules.System;
using Modules.System.IO;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Pokemon_3D_Server_Launcher_Core
{
    public sealed class Core
    {
        public Settings.Settings Settings { get; internal set; }
        public Logger.Logger Logger { get; private set; }
        public PlayerList.PlayerList PlayerList { get; private set; }

        public List<ICore> LoadedInstances { get; private set; } = new List<ICore>();

        private bool Stopping = false;

        public Core()
        {
            ExceptionHelper.Core = this;
            AppDomain.CurrentDomain.UnhandledException += (sender, ex) => { ((Exception)ex.ExceptionObject).CatchError(); };
            Logger = new Logger.Logger(this);
            Settings = new Settings.Settings(this);
            PlayerList = new PlayerList.PlayerList(this);
        }

        public void Start()
        {
            Task.Run(() =>
            {
                Settings.Start();
                Logger.Start();
                PlayerList.Start();

                foreach (string dll in Settings.ModulesToLoad)
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom($"{Settings.Directories.ModulesDirectory}/{dll}".GetFullPath());
                        List<TypeInfo> classImplmeneted = assembly.DefinedTypes.ToList();

                        foreach (TypeInfo @class in classImplmeneted)
                        {
                            if (@class.ImplementedInterfaces.Where(a => a.FullName == typeof(ICore).FullName).Count() > 0)
                            {
                                ICore newInstance = Activator.CreateInstance(@class.AsType(), null) as ICore;
                                newInstance.Start(this);
                                Logger.Log($"{newInstance.ModuleName} v{newInstance.ModuleVersion} have loaded.", "Info");
                                LoadedInstances.Add(newInstance);
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.CatchError();
                    }
                }
            });
        }

        public void Stop(int exitCode)
        {
            Task.Run(() =>
            {
                if (!Stopping)
                {
                    Stopping = true;

                    foreach (ICore loadedInstances in LoadedInstances)
                    {
                        try
                        {
                            loadedInstances.Stop(exitCode);
                        }
                        catch (Exception ex)
                        {
                            ex.CatchError();
                        }
                    }

                    Settings.Save();
                    Logger.Dispose();
                    PlayerList.Dispose();

                    Environment.Exit(exitCode);
                }
            }).Wait();
        }

        public object Invoke(string method, params object[] param)
        {
            return null;
        }
    }
}