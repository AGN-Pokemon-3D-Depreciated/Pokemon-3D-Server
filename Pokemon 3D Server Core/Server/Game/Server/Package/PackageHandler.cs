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
        private Tokens Token = Core.Settings.Server.Game.Tokens;

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

                case (int)PackageTypes.ChatMessage:
                    HandleChatMessage();
                    break;

                case (int)PackageTypes.Ping:
                    HandlePing();
                    break;

                case (int)PackageTypes.ServerDataRequest:
                    HandleServerDataRequest();
                    break;

                default:
                    Core.Logger.Debug("Unknown Package Data. Unable to proceed.", Package.Network);
                    break;
            }
        }

        private void HandleGameData()
        {
            if (Package.Network.IsActive)
            {
                if (Package.Network.Player != null)
                {
                    HandlePing();
                    Package.Network.Player.Update(Package, true);
                }
                else
                {
                    // New Player - Pending to join.
                    Player.Player player = new Player.Player(Core.TcpClientCollection.GameTcpClientCollection.NextPlayerID(), Package.Network, Package);
                    Package.Network.Player = player;

                    if (Core.TcpClientCollection.GameTcpClientCollection.ActivePlayer.Count() >= Core.Settings.Server.Game.Server.MaxPlayers)
                    {
                        KickUserJoin(player, "SERVER_FULL");
                        return;
                    }
                    else if (!Core.Settings.Server.Game.Server.GameModes.GameMode.Contains(player.GameMode, new NonCaseSensitiveHelper()))
                    {
                        KickUserJoin(player, "SERVER_WRONGGAMEMODE", Core.Settings.Server.Game.Server.GameModes.ToString());
                        return;
                    }
                    else if (!Core.Settings.Server.Game.Server.GameModes.OfflineMode && !player.IsGameJoltPlayer)
                    {
                        KickUserJoin(player, "SERVER_OFFLINEMODE");
                        return;
                    }
                    else if (Core.TcpClientCollection.GameTcpClientCollection.ActivePlayer.Where(a => a.Player.Name == player.Name && a.Player.GameJoltID == player.GameJoltID).Count() > 1)
                    {
                        KickUserJoin(player, "SERVER_CLONE");
                        return;
                    }

                    int playerID = player.PlayerInfo.ID;
                    string ipAddress = Package.Network.GetPublicIPFromClient();

                    if (Core.Settings.Server.Game.Features.WhiteList)
                    {
                        if (!WhiteList.Exists(playerID))
                        {
                            KickUserJoin(player, "SERVER_DISALLOW");
                            return;
                        }
                    }

                    if (Core.Settings.Server.Game.Features.BlackList)
                    {
                        if (BlackList.IsBanned(playerID))
                        {
                            KickUserJoin(player, "SERVER_BLACKLISTED", BlackList.GetReason(playerID), BlackList.GetDuration(playerID));
                            return;
                        }
                    }

                    if (Core.Settings.Server.Game.Features.IPBlackList)
                    {
                        if (IPBlackList.IsBanned(ipAddress))
                        {
                            KickUserJoin(player, "SERVER_IPBLACKLISTED", IPBlackList.GetReason(ipAddress), IPBlackList.GetDuration(ipAddress));
                            return;
                        }
                    }

                    player.Welcome();
                }
            }
        }

        private void KickUserJoin(Player.Player player, string tokenKey, params string[] tokenValue)
        {
            player.Network.SentToPlayer(new Package(PackageTypes.Kicked, Token.ToString(tokenKey, tokenValue), Package.Network));
            Core.Logger.Log(player.IsGameJoltPlayer ?
                Token.ToString("SERVER_GAMEJOLT", player.Name, player.GameJoltID, "is unable to join the server with the following reason: " + Token.ToString(tokenKey, tokenValue)) :
                Token.ToString("SERVER_NOGAMEJOLT", player.Name, "is unable to join the server with the following reason: " + Token.ToString(tokenKey, tokenValue)));
        }

        private void HandleChatMessage()
        {
            if (Core.Settings.Server.Game.Features.Chat.AllowChatInServer)
            {
                // Check if you are muted
                if (Core.Settings.Server.Game.Features.PlayerInfo && Core.Settings.Server.Game.Features.MuteList)
                {
                    lock (Core.SQLite.Connection)
                    {
                        TableQuery<MuteList> stm = Core.SQLite.Connection.Table<MuteList>().Where(a => a.PlayerID == Package.Network.Player.PlayerInfo.ID);
                        if (stm.Count() > 0)
                        {
                            MuteList mutelist = stm.First();
                            if (mutelist.Duration != -1)
                            {
                                if (DateTime.Now < mutelist.StartTime.AddSeconds(mutelist.Duration))
                                {
                                    TimeSpan duration = mutelist.StartTime.AddSeconds(mutelist.Duration) - DateTime.Now;
                                    //KickUserJoin(player, "SERVER_BLACKLISTED", blacklist.Reason, duration.ToString());
                                    return;
                                }
                                else
                                    Core.SQLite.Connection.Delete(mutelist);
                            }
                            else
                            {
                                //KickUserJoin(player, "SERVER_BLACKLISTED", blacklist.Reason, "Permanent");
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                Package.Network.SentToPlayer(new Package(PackageTypes.ChatMessage, -1, Token.ToString("SERVER_NOCHAT"), Package.Network));
            }
        }

        private void HandlePing()
        {
            Package.Network.Player.LastValidPing = DateTime.Now;
        }

        private void HandleServerDataRequest()
        {
            List<string> DataItems = new List<string>
            {
                Core.TcpClientCollection.GameTcpClientCollection.ActivePlayer.Count().ToString(),
                Core.Settings.Server.Game.Server.MaxPlayers == -1 ? int.MaxValue.ToString() : Core.Settings.Server.Game.Server.MaxPlayers.ToString(),
                Core.Settings.Server.Game.Server.ServerName,
                string.IsNullOrWhiteSpace(Core.Settings.Server.Game.Server.ServerMessage) ? "" : Core.Settings.Server.Game.Server.ServerMessage
            };

            if (Core.TcpClientCollection.GameTcpClientCollection.ActivePlayer.Count() > 0)
            {
                DataItems.AddRange(Core.TcpClientCollection.GameTcpClientCollection.ActivePlayer.Select(a =>
                {
                    return a.Player.IsGameJoltPlayer ? $"{a.Player.Name} ({a.Player.GameJoltID.ToString()})" : a.Player.Name;
                }));
            }

            Package.Network.SentToPlayer(new Package(PackageTypes.ServerInfoData, DataItems, Package.Network));
            Package.Network.Dispose();
        }
    }
}