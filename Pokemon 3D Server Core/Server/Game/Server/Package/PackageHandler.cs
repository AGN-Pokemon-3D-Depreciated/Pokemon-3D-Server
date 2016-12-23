using System.Collections.Generic;
using static Pokemon_3D_Server_Core.Server.Game.Server.Package.Package;

namespace Pokemon_3D_Server_Core.Server.Game.Server.Package
{
    public class PackageHandler
    {
        private Package Package;

        public PackageHandler(Package Package)
        {
            this.Package = Package;
            Handle();
        }

        private void Handle()
        {
            switch (Package.PackageType)
            {
                case (int)Package.PackageTypes.Unknown:
                    Core.Logger.Debug("Unknown Package Data. Unable to proceed.", Package.TcpClient);
                    break;

                case (int)Package.PackageTypes.GameData:
                    HandleGameData();
                    break;

                case (int)Package.PackageTypes.Ping:
                    HandlePing();
                    break;

                case (int)Package.PackageTypes.ServerDataRequest:
                    HandleServerDataRequest();
                    break;
            }
        }

        private void HandleGameData()
        {
            //if (Core.PlayerCollection.HasPlayer(Package.TcpClient))
            //    Core.PlayerCollection.GetPlayer(Package.TcpClient).Update(Package, true);
            //else
            //{
            //    // New Player - Pending to join.
            //    Player Player = new Player(Package);

            //    // Server Space Limit
            //    if (Core.PlayerCollection.Count >= Core.Setting.Server.MaxPlayers)
            //    {
            //        Core.PlayerCollection.SentToPlayer(new Package(Package.PackageTypes.Kicked, ServerMessage.Token(ServerMessage.Tokens.SERVER_FULL, null), Package.TcpClient));
            //        Core.Logger.Log(Player.isGamejoltPlayer ?
            //            ServerMessage.Token(ServerMessage.Tokens.SERVER_GAMEJOLT, Player.Name, Player.GamejoltID.ToString(), "is unable to join the server with the following reason: " + ServerMessage.Token(ServerMessage.Tokens.SERVER_FULL, null)) :
            //            ServerMessage.Token(ServerMessage.Tokens.SERVER_NOGAMEJOLT, Player.Name, "is unable to join the server with the following reason: " + ServerMessage.Token(ServerMessage.Tokens.SERVER_FULL, null)));

            //        return;
            //    }

            //    Core.PlayerCollection.Add(Package);
            //}
        }

        private void HandlePing()
        {
            //Core.PlayerCollection.GetPlayer(Package.TcpClient).PingCheck();
        }

        private void HandleServerDataRequest()
        {
            List<string> DataItems = new List<string>
            {
                "0", //Core.PlayerCollection.Count.ToString(),
                Core.Settings.Server.MaxPlayers == -1 ? int.MaxValue.ToString() : Core.Settings.Server.MaxPlayers.ToString(),
                Core.Settings.Server.ServerName,
                string.IsNullOrWhiteSpace(Core.Settings.Server.ServerMessage) ? "" : Core.Settings.Server.ServerMessage
            };

            //if (Core.PlayerCollection.Count > 0)
            //{
            //    for (int i = 0; i < Core.PlayerCollection.Count; i++)
            //    {
            //        DataItems.Add(Core.PlayerCollection[i].isGamejoltPlayer ? $"{Core.PlayerCollection[i].Name} ({Core.PlayerCollection[i].GamejoltID.ToString()})" : Core.PlayerCollection[i].Name);
            //    }
            //}

            Core.TcpClientCollection[Package.TcpClient].SentToPlayer(new Package(PackageTypes.ServerInfoData, DataItems, Package.TcpClient));
        }
    }
}
