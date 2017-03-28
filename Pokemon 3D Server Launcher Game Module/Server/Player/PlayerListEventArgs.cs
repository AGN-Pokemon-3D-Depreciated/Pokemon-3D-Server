namespace Pokemon_3D_Server_Launcher_Game_Module.Server.Player
{
    public class PlayerListEventArgs : Pokemon_3D_Server_Launcher_Core.PlayerList.PlayerListEventArgs
    {
        private Networking Network;

        public PlayerListEventArgs(Networking network, Operations operation, int id) : base(operation, id)
        {
            Network = network;
        }

        public override string AdditionalValue { get { return Network.Player.ToString(); } }
    }
}
