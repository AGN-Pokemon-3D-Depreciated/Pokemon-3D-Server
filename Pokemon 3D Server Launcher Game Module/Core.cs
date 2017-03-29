using Pokemon_3D_Server_Launcher_Core.Interfaces;

namespace Pokemon_3D_Server_Launcher_Game_Module
{
    public class Core
    {
        public Logger.Logger Logger { get; private set; }
        public Settings.Settings Settings { get; internal set; }
        public Server.TcpClientCollection TcpClientCollection { get; private set; }
        public SQLite.SQLite SQLite { get; private set; }
        public World.World World { get; private set; }
        public Server.TcpListener Listener { get; private set; }
        
        public ICore BaseCore { get; private set; }
        public Pokemon_3D_Server_Launcher_Core.Core BaseInstance { get { return BaseCore.BaseInstance; } }

        public Core(ICore instance)
        {
            BaseCore = instance;
            Logger = new Logger.Logger(this);
            Settings = new Settings.Settings(this);
            TcpClientCollection = new Server.TcpClientCollection(this);
            SQLite = new SQLite.SQLite(this);
            World = new World.World(this);
            Listener = new Server.TcpListener(this);
        }

        public void Start()
        {
            Settings.Start();
            SQLite.Start();
            World.Start();
            Listener.Start();
        }

        public void Stop(int exitCode)
        {
            Settings.Save();
            TcpClientCollection.Dispose();
            SQLite.Dispose();
            World.Dispose();
            Listener.Dispose();
        }
    }
}