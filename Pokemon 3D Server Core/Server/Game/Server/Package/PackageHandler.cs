using Modules.System.Net;
using Pokemon_3D_Server_Core.Modules.System.Collections.Generic;
using Pokemon_3D_Server_Core.Server.Game.SQLite.Tables;
using Pokemon_3D_Server_Core.Settings.Server.Game.Tokens;
using SQLite;
using System;
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
                    Player.Player player = new Player.Player(Core.TcpClientCollection.NextPlayerID(), Package.Network, Package);
                    Package.Network.Player = player;

                    Tokens token = Core.Settings.Server.Game.Tokens;

                    if (Core.TcpClientCollection.ActivePlayer.Count() >= Core.Settings.Server.Game.Server.MaxPlayers)
                        KickUserJoin(player, "SERVER_FULL");
                    else if (!Core.Settings.Server.Game.Server.GameModes.GameMode.Contains(player.GameMode, new NonCaseSensitiveHelper()))
                        KickUserJoin(player, "SERVER_WRONGGAMEMODE", Core.Settings.Server.Game.Server.GameModes.ToString());
                    else if (!Core.Settings.Server.Game.Server.GameModes.OfflineMode && !player.IsGameJoltPlayer)
                        KickUserJoin(player, "SERVER_OFFLINEMODE");
                    else if (Core.Settings.Server.Game.Features.WhiteList)
                    {
                        lock (Core.SQLite.Connection)
                        {
                            TableQuery<WhiteList> stm = Core.SQLite.Connection.Table<WhiteList>().Where(a => a.PlayerID == player.PlayerInfo.ID);
                            if (stm.Count() == 0)
                                KickUserJoin(player, "SERVER_DISALLOW");
                        }
                    }
                    else if (Core.TcpClientCollection.ActivePlayer.Where(a => a.Player.Name == player.Name && a.Player.GameJoltID == player.GameJoltID).Count() > 1)
                        KickUserJoin(player, "SERVER_CLONE");
                    else if (Core.Settings.Server.Game.Features.BlackList)
                    {
                        lock (Core.SQLite.Connection)
                        {
                            TableQuery<BlackList> stm = Core.SQLite.Connection.Table<BlackList>().Where(a => a.PlayerID == player.PlayerInfo.ID);
                            if (stm.Count() > 0)
                            {
                                BlackList blacklist = stm.First();
                                if (blacklist.Duration != -1)
                                {
                                    if (DateTime.Now < blacklist.StartTime.AddSeconds(blacklist.Duration))
                                    {
                                        TimeSpan duration = blacklist.StartTime.AddSeconds(blacklist.Duration) - DateTime.Now;
                                        KickUserJoin(player, "SERVER_BLACKLISTED", blacklist.Reason, duration.ToString());
                                    }
                                    else
                                        Core.SQLite.Connection.Delete(blacklist);
                                }
                                else
                                    KickUserJoin(player, "SERVER_BLACKLISTED", blacklist.Reason, "Permanent");
                            }
                        }
                    }
                    else if (Core.Settings.Server.Game.Features.IPBlackList)
                    {
                        lock (Core.SQLite.Connection)
                        {
                            TableQuery<IPBlackList> stm = Core.SQLite.Connection.Table<IPBlackList>().Where(a => a.IPAddress == Package.Network.GetPublicIPFromClient());
                            if (stm.Count() > 0)
                            {
                                IPBlackList blacklist = stm.First();
                                if (blacklist.Duration != -1)
                                {
                                    if (DateTime.Now < blacklist.StartTime.AddSeconds(blacklist.Duration))
                                    {
                                        TimeSpan duration = blacklist.StartTime.AddSeconds(blacklist.Duration) - DateTime.Now;
                                        KickUserJoin(player, "SERVER_IPBLACKLISTED", blacklist.Reason, duration.ToString());
                                    }
                                    else
                                        Core.SQLite.Connection.Delete(blacklist);
                                }
                                else
                                    KickUserJoin(player, "SERVER_IPBLACKLISTED", blacklist.Reason, "Permanent");
                            }
                        }
                    }

                    player.Welcome();
                }
            }
        }

        private void KickUserJoin(Player.Player player, string tokenKey, params string[] tokenValue)
        {
            Tokens token = Core.Settings.Server.Game.Tokens;
            player.Network.SentToPlayer(new Package(PackageTypes.Kicked, token.ToString(tokenKey, tokenValue), Package.Network));
            Core.Logger.Log(player.IsGameJoltPlayer ?
                token.ToString("SERVER_GAMEJOLT", player.Name, player.GameJoltID, "is unable to join the server with the following reason: " + token.ToString(tokenKey, tokenValue)) :
                token.ToString("SERVER_NOGAMEJOLT", player.Name, "is unable to join the server with the following reason: " + token.ToString(tokenKey, tokenValue)));
        }

        private void HandlePing()
        {
            //Core.PlayerCollection.GetPlayer(Package.TcpClient).PingCheck();
        }

        private void HandleServerDataRequest()
        {
            List<string> DataItems = new List<string>
            {
                Core.TcpClientCollection.ActivePlayer.Count().ToString(),
                Core.Settings.Server.Game.Server.MaxPlayers == -1 ? int.MaxValue.ToString() : Core.Settings.Server.Game.Server.MaxPlayers.ToString(),
                Core.Settings.Server.Game.Server.ServerName,
                string.IsNullOrWhiteSpace(Core.Settings.Server.Game.Server.ServerMessage) ? "" : Core.Settings.Server.Game.Server.ServerMessage
            };

            if (Core.TcpClientCollection.ActivePlayer.Count() > 0)
            {
                DataItems.AddRange(Core.TcpClientCollection.ActivePlayer.Select(a =>
                {
                    return a.Player.IsGameJoltPlayer ? $"{a.Player.Name} ({a.Player.GameJoltID.ToString()})" : a.Player.Name;
                }));
            }

            Package.Network.SentToPlayer(new Package(PackageTypes.ServerInfoData, DataItems, Package.Network));
            Package.Network.Dispose();
        }
    }
}
