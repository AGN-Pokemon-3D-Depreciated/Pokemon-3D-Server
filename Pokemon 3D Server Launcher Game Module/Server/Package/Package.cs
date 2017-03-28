using Modules.System;
using Pokemon_3D_Server_Core.Modules.System.Collections.Generic;
using Pokemon_3D_Server_Launcher_Game_Module.Server.Player;
using Pokemon_3D_Server_Launcher_Game_Module.SQLite.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using static Pokemon_3D_Server_Launcher_Core.PlayerList.PlayerListEventArgs;

namespace Pokemon_3D_Server_Launcher_Game_Module.Server.Package
{
    public class Package
    {
        private Core Core;

        public int PackageType { get; private set; } = (int)PackageTypes.Unknown;
        public int Origin { get; private set; } = -1;
        public int DataItemsCount { get { return DataItems.Count; } }
        public List<string> DataItems { get; private set; } = new List<string>();
        public bool IsValid { get; private set; }
        public Networking Network { get; private set; }

        public enum PackageTypes
        {
            Unknown = -1,

            /// <summary>
            /// Package Type: Game Data
            /// <para>Join: {Origin = PlayerID | DataItem[] = FullPackageData[] | To other players}</para>
            /// <para>Update: {Origin = PlayerID | DataItem[] = PartialPackageData[] | To other players}</para>
            /// </summary>
            GameData = 0,

            /// <summary>
            /// Private Message
            /// <para>Global: {Origin = -1 | DataItem[0] = Message | To the player}</para>
            /// <para>Own: {Origin = PlayerID | DataItem[0] = PlayerID, DataItem[1] = Message | To yourself}</para>
            /// <para>Client: {Origin = PlayerID | DataItem[0] = Message | To client}</para>
            /// </summary>
            PrivateMessage = 2,

            /// <summary>
            /// Chat Message
            /// <para>Global: {Origin = -1 | DataItem[0] = Message | To all players}</para>
            /// <para>Player: {Origin = PlayerID | DataItem[0] = Message | To all players}</para>
            /// </summary>
            ChatMessage = 3,

            /// <summary>
            /// Kick
            /// <para>{Origin = -1 | DataItem[0] = Reason | To player}</para>
            /// </summary>
            Kicked = 4,

            /// <summary>
            /// ID
            /// <para>{Origin = -1 | DataItem[0] = PlayerID | To own}</para>
            /// </summary>
            ID = 7,

            /// <summary>
            /// Create Player
            /// <para>{Origin = -1 | DataItem[0] = PlayerID | To other players}</para>
            /// </summary>
            CreatePlayer = 8,

            /// <summary>
            /// Destroy Player
            /// <para>{Origin = -1 | DataItem[0] = PlayerID | To other players}</para>
            /// </summary>
            DestroyPlayer = 9,

            /// <summary>
            /// Server Close
            /// <para>{Origin = -1 | DataItem[0] = Reason | To all players}</para>
            /// </summary>
            ServerClose = 10,

            /// <summary>
            /// Server Message
            /// <para>{Origin = -1 | DataItem[0] = Message | To all players}</para>
            /// </summary>
            ServerMessage = 11,

            /// <summary>
            /// World Data
            /// <para>{Origin = -1 | DataItem[0] = Season, DataItem[1] = Weather, DataItem[2] = Time | To all players}</para>
            /// </summary>
            WorldData = 12,

            /// <summary>
            /// Ping (Get Only)
            /// </summary>
            Ping = 13,

            /// <summary>
            /// Gamestate Message (Get Only)
            /// </summary>
            GamestateMessage = 14,

            /// <summary>
            /// Trade Request
            /// <para>{Origin = PlayerID | DataItem = null | To trade player}</para>
            /// </summary>
            TradeRequest = 30,

            /// <summary>
            /// Trade Join
            /// <para>{Origin = PlayerID | DataItem = null | To trade player}</para>
            /// </summary>
            TradeJoin = 31,

            /// <summary>
            /// Trade Quit
            /// <para>{Origin = PlayerID | DataItem = null | To trade player}</para>
            /// </summary>
            TradeQuit = 32,

            /// <summary>
            /// Trade Offer
            /// <para>{Origin = PlayerID | DataItem[0] = PokemonData | To trade player}</para>
            /// </summary>
            TradeOffer = 33,

            /// <summary>
            /// Trade Start
            /// <para>{Origin = PlayerID | DataItem = null | To trade player}</para>
            /// </summary>
            TradeStart = 34,

            /// <summary>
            /// Battle Request
            /// <para>{Origin = PlayerID | DataItem = null | To battle player}</para>
            /// </summary>
            BattleRequest = 50,

            /// <summary>
            /// Battle Join
            /// <para>{Origin = PlayerID | DataItem = null | To battle player}</para>
            /// </summary>
            BattleJoin = 51,

            /// <summary>
            /// Battle Quit
            /// <para>{Origin = PlayerID | DataItem = null | To battle player}</para>
            /// </summary>
            BattleQuit = 52,

            /// <summary>
            /// Battle Offer
            /// <para>{Origin = PlayerID | DataItem[0] = PokemonData | To battle player}</para>
            /// </summary>
            BattleOffer = 53,

            /// <summary>
            /// Battle Start
            /// <para>{Origin = PlayerID | DataItem = null | To battle player}</para>
            /// </summary>
            BattleStart = 54,

            /// <summary>
            /// Battle Client Data
            /// <para>{Origin = PlayerID | DataItem[0] = ClientData | To battle player}</para>
            /// </summary>
            BattleClientData = 55,

            /// <summary>
            /// Battle Host Data
            /// <para>{Origin = PlayerID | DataItem[0] = HostData | To battle player}</para>
            /// </summary>
            BattleHostData = 56,

            /// <summary>
            /// Battle Pokemon Data
            /// <para>{Origin = PlayerID | DataItem[0] = PokemonData | To battle player}</para>
            /// </summary>
            BattlePokemonData = 57,

            /// <summary>
            /// Server Info Data
            /// <para>{Origin = -1 | DataItem[] = Server Info | To listening client}</para>
            /// </summary>
            ServerInfoData = 98,

            /// <summary>
            /// Server Data Request (Read only)
            /// </summary>
            ServerDataRequest = 99,
        }

        public Package(Core core, string fullData, Networking network)
        {
            try
            {
                Core = core;
                Network = network;

                if (fullData == null || !fullData.Contains("|"))
                {
                    Core.Logger.Debug($"Package is incomplete.", Network);
                    IsValid = false;
                    return;
                }

                List<string> bits = fullData.Split('|').ToList();

                if (bits.Count >= 5)
                {
                    // Protocol Version
                    if (!string.Equals(Core.Settings.Server.ProtocolVersion, bits[0], StringComparison.OrdinalIgnoreCase))
                    {
                        Core.Logger.Debug($"Package does not contains valid Protocol Version.", Network);
                        IsValid = false;
                        return;
                    }

                    // Package Type
                    try
                    {
                        PackageType = int.Parse(bits[1]);
                    }
                    catch (Exception)
                    {
                        Core.Logger.Debug($"Package does not contains valid Package Type.", Network);
                        IsValid = false;
                        return;
                    }

                    // Origin
                    try
                    {
                        Origin = int.Parse(bits[2]);
                    }
                    catch (Exception)
                    {
                        Core.Logger.Debug($"Package does not contains valid Origin.", Network);
                        IsValid = false;
                        return;
                    }

                    // DataItemsCount
                    int DataItemsCount = 0;

                    try
                    {
                        DataItemsCount = int.Parse(bits[3]);
                    }
                    catch (Exception)
                    {
                        Core.Logger.Debug($"Package does not contains valid DataItemsCount.", Network);
                        IsValid = false;
                        return;
                    }

                    List<int> offsetList = new List<int>();

                    // Count from 4th item to second last item. Those are the offsets.
                    for (int i = 4; i < DataItemsCount + 4; i++)
                    {
                        try
                        {
                            offsetList.Add(bits[i].ToInt());
                        }
                        catch (Exception)
                        {
                            Core.Logger.Debug($"Package does not contains valid Offset.", Network);
                            IsValid = false;
                            return;
                        }
                    }

                    // Set the datastring, its the last item in the list. If it contained any separators, they will get readded here:
                    string dataString = null;

                    for (int i = DataItemsCount + 4; i < bits.Count; i++)
                    {
                        if (i > DataItemsCount + 4)
                            dataString += "|";

                        dataString += bits[i];
                    }

                    // Cutting the data:
                    for (int i = 0; i < offsetList.Count; i++)
                    {
                        int cOffset = offsetList[i];
                        int length = dataString.Length - cOffset;

                        if (i < offsetList.Count - 1)
                            length = offsetList[i + 1] - cOffset;

                        DataItems.Add(dataString.Substring(cOffset, length));
                    }

                    IsValid = true;
                }
                else
                {
                    Core.Logger.Debug($"Package is incomplete.", Network);
                    IsValid = false;
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Debug(ex.Message, Network);
                IsValid = false;
            }
        }

        public Package(Core core, PackageTypes packageType, int origin, List<string> dataItems, Networking network = null)
        {
            Core = core;
            PackageType = (int)packageType;
            Origin = origin;
            DataItems = dataItems;
            Network = network;
            IsValid = true;
        }

        public Package(Core core, PackageTypes packageType, List<string> dataItems, Networking network = null)
        {
            Core = core;
            PackageType = (int)packageType;
            DataItems = dataItems;
            Network = network;
            IsValid = true;
        }

        public Package(Core core, PackageTypes packageType, int origin, string dataItems, Networking network = null)
        {
            Core = core;
            PackageType = (int)packageType;
            Origin = origin;
            DataItems = new List<string> { dataItems };
            Network = network;
            IsValid = true;
        }

        public Package(Core core, PackageTypes packageType, string dataItems, Networking network = null)
        {
            Core = core;
            PackageType = (int)packageType;
            DataItems = new List<string> { dataItems };
            Network = network;
            IsValid = true;
        }

        public void Handle()
        {
            try
            {
                switch (PackageType)
                {
                    case (int)PackageTypes.Unknown:
                        Core.Logger.Debug("Unknown Package Data. Unable to proceed.", Network);
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
                        Core.Logger.Debug("Unknown Package Data. Unable to proceed.", Network);
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.CatchError();
            }
        }

        public bool IsFullPackageData()
        {
            if (DataItems.Count == 15 && !string.IsNullOrWhiteSpace(DataItems[4]))
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            string outputStr = Core.Settings.Server.ProtocolVersion + "|" + PackageType.ToString() + "|" + Origin.ToString() + "|" + DataItemsCount.ToString();

            int currentIndex = 0;
            string data = null;

            foreach (string dataItem in DataItems)
            {
                outputStr += "|" + currentIndex.ToString();
                data += dataItem;
                currentIndex += dataItem.Length;
            }

            outputStr += "|" + data;

            return outputStr;
        }

        private void HandleGameData()
        {
            if (Network.IsActive)
            {
                if (Network.Player != null)
                {
                    HandlePing();
                    Network.Player.Update(this, true);
                    Core.BaseInstance.PlayerList.Update(new PlayerListEventArgs(Network, Operations.Update, Network.Player.ID));
                }
                else
                {
                    // New Player - Pending to join.
                    Player.Player player = new Player.Player(Core, Core.TcpClientCollection.NextPlayerID(), Network, this);
                    Network.Player = player;
                    Core.BaseInstance.PlayerList.Update(new PlayerListEventArgs(Network, Operations.Add, Network.Player.ID));

                    if (Core.TcpClientCollection.ActivePlayer.Count() >= Core.Settings.Server.MaxPlayers)
                    {
                        KickUserJoin(player, "SERVER_FULL");
                        return;
                    }
                    else if (!Core.Settings.Server.GameModes.GetGameMode().Contains(player.GameMode, new NonCaseSensitiveHelper()))
                    {
                        KickUserJoin(player, "SERVER_WRONGGAMEMODE", Core.Settings.Server.GameModes.ToString());
                        return;
                    }
                    else if (!Core.Settings.Server.GameModes.OfflineMode && !player.IsGameJoltPlayer)
                    {
                        KickUserJoin(player, "SERVER_OFFLINEMODE");
                        return;
                    }
                    else if (Core.TcpClientCollection.ActivePlayer.Where(a => a.Player.Name == player.Name && a.Player.GameJoltID == player.GameJoltID).Count() > 1)
                    {
                        KickUserJoin(player, "SERVER_CLONE");
                        return;
                    }

                    int playerID = player.PlayerInfo.ID;
                    string ipAddress = Network.GetPublicIPFromClient();

                    if (Core.Settings.Features.WhiteList)
                    {
                        if (Core.SQLite.Query<WhiteList>(a => a.PlayerID == playerID).Count == 0)
                        {
                            KickUserJoin(player, "SERVER_DISALLOW");
                            return;
                        }
                    }

                    if (Core.Settings.Features.BlackList)
                    {
                        List<BlackList> item = Core.SQLite.Query<BlackList>(a => a.PlayerID == playerID);
                        if (item.Count > 0)
                        {
                            if (DateTime.Now < (item[0].Duration > 0 ? item[0].StartTime.AddSeconds(item[0].Duration) : DateTime.MaxValue))
                            {
                                KickUserJoin(player, "SERVER_BLACKLISTED", item[0].Reason, ((item[0].Duration > 0 ? item[0].StartTime.AddSeconds(item[0].Duration) : DateTime.MaxValue) - DateTime.Now).ToString("d\\:hh\\:mm\\:ss"));
                                return;
                            }
                        }
                    }

                    if (Core.Settings.Features.IPBlackList)
                    {
                        List<IPBlackList> item = Core.SQLite.Query<IPBlackList>(a => a.IPAddress == ipAddress);
                        if (item.Count > 0)
                        {
                            if (DateTime.Now < (item[0].Duration > 0 ? item[0].StartTime.AddSeconds(item[0].Duration) : DateTime.MaxValue))
                            {
                                KickUserJoin(player, "SERVER_IPBLACKLISTED", item[0].Reason, ((item[0].Duration > 0 ? item[0].StartTime.AddSeconds(item[0].Duration) : DateTime.MaxValue) - DateTime.Now).ToString("d\\:hh\\:mm\\:ss"));
                                return;
                            }
                        }
                    }

                    player.Welcome();
                }
            }
        }

        private void KickUserJoin(Player.Player player, string tokenKey, params string[] tokenValue)
        {
            player.Network.SentToPlayer(new Package(Core, PackageTypes.Kicked, Core.Settings.Tokens.ToString(tokenKey, tokenValue), Network));
            Core.Logger.Log(player.IsGameJoltPlayer ?
                Core.Settings.Tokens.ToString("SERVER_GAMEJOLT", player.Name, player.GameJoltID, "is unable to join the server with the following reason: " + Core.Settings.Tokens.ToString(tokenKey, tokenValue)) :
                Core.Settings.Tokens.ToString("SERVER_NOGAMEJOLT", player.Name, "is unable to join the server with the following reason: " + Core.Settings.Tokens.ToString(tokenKey, tokenValue)));
        }

        private void HandleChatMessage()
        {
            if (Core.Settings.Features.Chat.AllowChatInServer)
            {
                Player.Player player = Network.Player;
                int playerID = Network.Player.PlayerInfo.ID;

                // Check if you are muted
                if (Core.Settings.Features.MuteList)
                {
                    List<MuteList> item = Core.SQLite.Query<MuteList>(a => a.MuteID == playerID);
                    if (item.Count > 0)
                    {
                        for (int i = 0; i < item.Count; i++)
                        {
                            if (DateTime.Now < (item[i].Duration > 0 ? item[i].StartTime.AddSeconds(item[i].Duration) : DateTime.MaxValue))
                            {
                                Network.SentToPlayer(new Package(Core, PackageTypes.ChatMessage, -1, Core.Settings.Tokens.ToString(item[i].PlayerID == -1 ? "SERVER_MUTED" : "SERVER_MUTEDTEMP", item[i].Reason, ((item[i].Duration > 0 ? item[i].StartTime.AddSeconds(item[i].Duration) : DateTime.MaxValue) - DateTime.Now).ToString("d\\:hh\\:mm\\:ss")), Network));
                                Core.Logger.Log(player.IsGameJoltPlayer ?
                                    Core.Settings.Tokens.ToString("SERVER_GAMEJOLT", player.Name, player.GameJoltID, "is unable to chat in the server with the following reason: " + Core.Settings.Tokens.ToString(item[i].PlayerID == -1 ? "SERVER_MUTED" : "SERVER_MUTEDTEMP", item[i].Reason, ((item[i].Duration > 0 ? item[i].StartTime.AddSeconds(item[i].Duration) : DateTime.MaxValue) - DateTime.Now).ToString("d\\:hh\\:mm\\:ss"))) :
                                    Core.Settings.Tokens.ToString("SERVER_NOGAMEJOLT", player.Name, "is unable to chat in the server with the following reason: " + Core.Settings.Tokens.ToString(item[i].PlayerID == -1 ? "SERVER_MUTED" : "SERVER_MUTEDTEMP", item[i].Reason, ((item[i].Duration > 0 ? item[i].StartTime.AddSeconds(item[i].Duration) : DateTime.MaxValue) - DateTime.Now).ToString("d\\:hh\\:mm\\:ss"))));
                            }
                        }
                    }
                }


            }
            else
            {
                Network.SentToPlayer(new Package(Core, PackageTypes.ChatMessage, -1, Core.Settings.Tokens.ToString("SERVER_NOCHAT"), Network));
            }
        }

        private void HandlePing()
        {
            Network.Player.LastValidPing = DateTime.Now;
        }

        private void HandleServerDataRequest()
        {
            List<string> DataItems = new List<string>
            {
                Core.TcpClientCollection.ActivePlayer.Count().ToString(),
                Core.Settings.Server.MaxPlayers == -1 ? int.MaxValue.ToString() : Core.Settings.Server.MaxPlayers.ToString(),
                Core.Settings.Server.ServerName,
                string.IsNullOrWhiteSpace(Core.Settings.Server.ServerMessage) ? "" : Core.Settings.Server.ServerMessage
            };

            if (Core.TcpClientCollection.ActivePlayer.Count() > 0)
            {
                DataItems.AddRange(Core.TcpClientCollection.ActivePlayer.Select(a =>
                {
                    return a.Player.IsGameJoltPlayer ? $"{a.Player.Name} ({a.Player.GameJoltID.ToString()})" : a.Player.Name;
                }));
            }

            Network.SentToPlayer(new Package(Core, PackageTypes.ServerInfoData, DataItems, Network));
            Network.Dispose();
        }
    }
}
