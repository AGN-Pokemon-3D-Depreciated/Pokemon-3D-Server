using Modules.System.Threading;
using Pokemon_3D_Server_Core.Interface;
using Pokemon_3D_Server_Core.Server.Game.Server;
using Pokemon_3D_Server_Core.Server.Game.SQLite;
using Pokemon_3D_Server_Core.Server.Game.World;
using System.Collections.Generic;

namespace Pokemon_3D_Server_Core
{
    public class Core
    {
        #region Core
        /// <summary>
        /// Get Application Settings.
        /// </summary>
        public static Settings.Settings Settings { get; internal set; }

        /// <summary>
        /// Get Application Logger.
        /// </summary>
        public static Logger.Logger Logger { get; private set; }
        #endregion Core

        #region Server
        #region Game
        /// <summary>
        /// Get Game Database.
        /// </summary>
        public static Database Database { get; private set; }

        /// <summary>
        /// Get Game World.
        /// </summary>
        public static World World { get; private set; }

        /// <summary>
        /// Get Game Server TcpClientCollection.
        /// </summary>
        public static TcpClientCollection TcpClientCollection { get; private set; }

        /// <summary>
        /// Get Game Server Listener.
        /// </summary>
        public static Listener Listener { get; private set; }
        #endregion Game
        #endregion Server

        private List<IModules> IModules = new List<IModules>();
        private ThreadHelper Thread = new ThreadHelper();

        public Core()
        {
            Settings = new Settings.Settings();
            Logger = new Logger.Logger();

            Database = new Database();
            World = new World();
            TcpClientCollection = new TcpClientCollection();
            Listener = new Listener();

            IModules.AddRange(new List<IModules> { Settings, Database, World, Listener, Logger });
        }

        /// <summary>
        /// Start Core Application.
        /// </summary>
        public void Start()
        {
            Thread.Add(() =>
            {
                foreach (IModules item in IModules)
                {
                    item.Start();
                    Logger.Log($"Module: {item.Name} v{item.Version} is started.");
                }
            });
        }

        /// <summary>
        /// Stop Core Application.
        /// </summary>
        public void Stop()
        {
            foreach (IModules item in IModules)
            {
                item.Stop();
                Logger.Log($"Module: {item.Name} v{item.Version} is stopped.");
            }
        }
    }
}
