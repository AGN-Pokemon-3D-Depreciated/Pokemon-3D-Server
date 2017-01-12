using Pokemon_3D_Server_Core.Settings.Server.Game.Features;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using static Pokemon_3D_Server_Core.Server.Game.Server.Package.Package;

namespace Pokemon_3D_Server_Core.Server.Game.Server.Package
{
    public class PackageHandler
    {
        private Package Package;

        public PackageHandler(Package package)
        {
            Package = package;
            Handle();
        }

        private void Handle()
        {
            switch (Package.PackageType)
            {
                case (int)PackageTypes.Unknown:
                    Core.Logger.Debug("Unknown Package Data. Unable to proceed.", Package.Network);
                    break;

                case (int)PackageTypes.GameData:
                    HandleGameData();
                    break;

                case (int)PackageTypes.Ping:
                    HandlePing();
                    break;

                case (int)PackageTypes.ServerDataRequest:
                    HandleServerDataRequest();
                    break;
            }
        }

        private void HandleGameData()
        {
            if (Package.Network.IsActive)
            {
                if (Package.Network.Player != null)
                {
                    // Player exist, just update :)
                }
                else
                {
                    // New Player - Pending to join.
                    Package.Network.Player = new Player.Player(Core.TcpClientCollection.NextPlayerID(), Package.Network, Package);

                    // 1. Check server space limit.
                    if (Core.TcpClientCollection.ActivePlayerCount >= Core.Settings.Server.Game.Server.MaxPlayers)
                    {
                        Package.Network.SentToPlayer(new Package(PackageTypes.Kicked, Core.Settings.Server.Game.Tokens.ToString("SERVER_FULL"), Package.Network));
                        Core.Logger.Log(Package.Network.Player.IsGameJoltPlayer ?
                            Core.Settings.Server.Game.Tokens.ToString("SERVER_GAMEJOLT", Package.Network.Player.Name, Package.Network.Player.GameJoltID.ToString(), "is unable to join the server with the following reason: " + Core.Settings.Server.Game.Tokens.ToString("SERVER_FULL")) :
                            Core.Settings.Server.Game.Tokens.ToString("SERVER_NOGAMEJOLT", Package.Network.Player.Name, "is unable to join the server with the following reason: " + Core.Settings.Server.Game.Tokens.ToString("SERVER_FULL")));

                        return;
                    }

                    // 2. Check blacklist.
                    if (Core.Settings.Server.Game.Features.BlackList)
                    {
                        
                    }
                }
            }
        }

        private void HandlePing()
        {
            //Core.PlayerCollection.GetPlayer(Package.TcpClient).PingCheck();
        }

        private void HandleServerDataRequest()
        {
            List<string> DataItems = new List<string>
            {
                Core.TcpClientCollection.ActivePlayerCount.ToString(),
                Core.Settings.Server.Game.Server.MaxPlayers == -1 ? int.MaxValue.ToString() : Core.Settings.Server.Game.Server.MaxPlayers.ToString(),
                Core.Settings.Server.Game.Server.ServerName,
                string.IsNullOrWhiteSpace(Core.Settings.Server.Game.Server.ServerMessage) ? "" : Core.Settings.Server.Game.Server.ServerMessage
            };

            if (Core.TcpClientCollection.ActivePlayerCount > 0)
            {
                DataItems.AddRange(Core.TcpClientCollection.Where(a => a.Value.Player != null).Select(a =>
                {
                    return a.Value.Player.IsGameJoltPlayer ? $"{a.Value.Player.Name} ({a.Value.Player.GameJoltID.ToString()})" : a.Value.Player.Name;
                }));
            }

            Package.Network.SentToPlayer(new Package(PackageTypes.ServerInfoData, DataItems, Package.Network));
            Package.Network.Dispose();
        }
    }
}
