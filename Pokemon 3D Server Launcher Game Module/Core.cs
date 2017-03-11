using Pokemon_3D_Server_Launcher_Core.Interfaces;

namespace Pokemon_3D_Server_Launcher_Game_Module
{
    public class Core
    {
        public Settings.Settings Settings { get; internal set; }
        public Logger.Logger Logger { get; private set; }

        public ICore BaseCore { get; private set; }
        public Pokemon_3D_Server_Launcher_Core.Core BaseInstance { get { return BaseCore.BaseInstance; } }

        public Core(ICore instance)
        {
            BaseCore = instance;
            Logger = new Logger.Logger(this);
            Settings = new Settings.Settings(this);
        }

        public void Stop(int exitCode)
        {
            Settings.Save();
        }
    }
}