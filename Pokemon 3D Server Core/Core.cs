using Modules.System;
using Modules.System.Threading;
using Pokemon_3D_Server_Core.Interface;
using Pokemon_3D_Server_Core.Server.Game.Server;
using Pokemon_3D_Server_Core.Server.Game.World;
using System;
using System.Collections.Generic;

namespace Pokemon_3D_Server_Core
{
    public class Core
    {
        #region Core
        public static Settings.Settings Settings { get; internal set; }
        public static Logger.Logger Logger { get; private set; }
        #endregion Core

        #region Server
        #region Game
        public static Server.Game.SQLite.SQLite SQLite { get; private set; }
        public static World World { get; private set; }
        public static TcpClientCollection TcpClientCollection { get; private set; }
        public static Listener Listener { get; private set; }
        #endregion Game
        #endregion Server

        private List<IModules> IModules = new List<IModules>();
        private ThreadHelper Thread = new ThreadHelper();

        public Core()
        {
            try
            {
                Settings = new Settings.Settings();
                Logger = new Logger.Logger();

                SQLite = new Server.Game.SQLite.SQLite();
                World = new World();
                TcpClientCollection = new TcpClientCollection();
                Listener = new Listener();

                IModules.AddRange(new List<IModules> { Settings, SQLite, World, Listener, Logger });
            }
            catch (Exception ex)
            {
                ex.CatchError();
            }
        }

        public void Start()
        {
            Thread.Add(() =>
            {
                foreach (IModules item in IModules)
                {
                    try
                    {
                        item.Start();
                        Logger.Log($"Module: {item.Name} v{item.Version} is started.");
                    }
                    catch (Exception ex)
                    {
                        ex.CatchError();
                    }
                }
            });
        }

        public void Stop()
        {
            foreach (IModules item in IModules)
            {
                try
                {
                    item.Stop();
                    Logger.Log($"Module: {item.Name} v{item.Version} is stopped.");
                }
                catch (Exception ex)
                {
                    ex.CatchError();
                }
            }
        }
    }
}
