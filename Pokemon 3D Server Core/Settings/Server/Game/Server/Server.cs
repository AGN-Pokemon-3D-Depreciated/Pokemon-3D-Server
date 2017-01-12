using Modules.System;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Server
{
    public class Server
    {
        public string ServerName { get; set; } = "P3D Server";
        public string ServerMessage { get; set; } = "A customized P3D Server.";
        public string WelcomeMessage { get; set; } = "Welcome to P3D Server. If you need help configuring the server, ask jianmingyong for more details.";
        public GameModes.GameModes GameModes { get; private set; } = new GameModes.GameModes();

        private int _MaxPlayers = 20;
        public int MaxPlayers
        {
            get { return _MaxPlayers; }
            set
            {
                if (value <= 0)
                    _MaxPlayers = int.MaxValue;
                else
                    _MaxPlayers = value.Clamp(1, int.MaxValue);
            }
        }

        
    }
}
