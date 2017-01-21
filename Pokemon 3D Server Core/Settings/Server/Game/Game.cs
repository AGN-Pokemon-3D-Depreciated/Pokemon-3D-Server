namespace Pokemon_3D_Server_Core.Settings.Server.Game
{
    public class Game
    {
        public Network.Network Network { get; private set; } = new Network.Network();
        public Server.Server Server { get; private set; } = new Server.Server();
        public Tokens.Tokens Tokens { get; private set; } = new Tokens.Tokens();
        public World.World World { get; private set; } = new World.World();
        public Features.Features Features { get; set; } = new Features.Features();
        public Logger.Logger Logger { get; set; } = new Logger.Logger();
    }
}