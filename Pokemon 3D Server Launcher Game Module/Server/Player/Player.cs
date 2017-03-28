using Modules.System;
using Pokemon_3D_Server_Launcher_Core.Modules.System.Threading;
using Pokemon_3D_Server_Launcher_Core.PlayerList;
using Pokemon_3D_Server_Launcher_Game_Module.SQLite.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using static Pokemon_3D_Server_Launcher_Core.PlayerList.PlayerListEventArgs;
using static Pokemon_3D_Server_Launcher_Game_Module.Server.Package.Package;

namespace Pokemon_3D_Server_Launcher_Game_Module.Server.Player
{
    public partial class Player : IDisposable
    {
        private Core Core;

        #region Player Data

        /// <summary>
        /// Get/Set Player DataItem[0]
        /// </summary>
        public string GameMode { get; set; }

        private int _IsGameJoltPlayer;

        /// <summary>
        /// Get/Set Player DataItem[1]
        /// </summary>
        public bool IsGameJoltPlayer
        {
            get { return _IsGameJoltPlayer.ToBool(); }
            set { _IsGameJoltPlayer = value.ToInt(); }
        }

        /// <summary>
        /// Get/Set Player DataItem[2]
        /// </summary>
        public string GameJoltID { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[3]
        /// </summary>
        public string DecimalSeparator { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[4]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[5]
        /// </summary>
        public string LevelFile { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[6]
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Get/Set Player Position X
        /// </summary>
        public double Position_X
        {
            get { return Position.GetSplit(0).ToDouble(); }
            set { Position = value.ToString().ConvertStringCulture(DecimalSeparator) + "|" + Position_Y.ToString().ConvertStringCulture(DecimalSeparator) + "|" + Position_Z.ToString().ConvertStringCulture(DecimalSeparator); }
        }

        /// <summary>
        /// Get/Set Player Position Y
        /// </summary>
        public double Position_Y
        {
            get { return Position.GetSplit(1).ToDouble(); }
            set { Position = Position_X.ToString().ConvertStringCulture(DecimalSeparator) + "|" + value.ToString().ConvertStringCulture(DecimalSeparator) + "|" + Position_Z.ToString().ConvertStringCulture(DecimalSeparator); }
        }

        /// <summary>
        /// Get/Set Player Position Z
        /// </summary>
        public double Position_Z
        {
            get { return Position.GetSplit(2).ToDouble(); }
            set { Position = Position_X.ToString().ConvertStringCulture(DecimalSeparator) + "|" + Position_Y.ToString().ConvertStringCulture(DecimalSeparator) + "|" + value.ToString().ConvertStringCulture(DecimalSeparator); }
        }

        /// <summary>
        /// Get/Set Player DataItem[7]
        /// </summary>
        public int Facing { get; set; }

        private int _Moving;

        /// <summary>
        /// Get/Set Player DataItem[8]
        /// </summary>
        public bool Moving
        {
            get { return _Moving.ToBool(); }
            set { _Moving = value.ToInt(); }
        }

        /// <summary>
        /// Get/Set Player DataItem[9]
        /// </summary>
        public string Skin { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[10]
        /// </summary>
        public int BusyType { get; set; }

        private int _PokemonVisible;

        /// <summary>
        /// Get/Set Player DataItem[11]
        /// </summary>
        public bool PokemonVisible
        {
            get { return _PokemonVisible.ToBool(); }
            set { _PokemonVisible = value.ToInt(); }
        }

        /// <summary>
        /// Get/Set Player DataItem[12]
        /// </summary>
        public string PokemonPosition { get; set; }

        /// <summary>
        /// Get/Set Player PokemonPosition X
        /// </summary>
        public double PokemonPosition_X
        {
            get { return PokemonPosition.GetSplit(0).ToDouble(); }
            set { PokemonPosition = value.ToString().ConvertStringCulture(DecimalSeparator) + "|" + PokemonPosition_Y.ToString().ConvertStringCulture(DecimalSeparator) + "|" + PokemonPosition_Z.ToString().ConvertStringCulture(DecimalSeparator); }
        }

        /// <summary>
        /// Get/Set Player PokemonPosition Y
        /// </summary>
        public double PokemonPosition_Y
        {
            get { return PokemonPosition.GetSplit(1).ToDouble(); }
            set { PokemonPosition = PokemonPosition_X.ToString().ConvertStringCulture(DecimalSeparator) + "|" + value.ToString().ConvertStringCulture(DecimalSeparator) + "|" + PokemonPosition_Z.ToString().ConvertStringCulture(DecimalSeparator); }
        }

        /// <summary>
        /// Get/Set Player PokemonPosition Z
        /// </summary>
        public double PokemonPosition_Z
        {
            get { return PokemonPosition.GetSplit(2).ToDouble(); }
            set { PokemonPosition = PokemonPosition_X.ToString().ConvertStringCulture(DecimalSeparator) + "|" + PokemonPosition_Y.ToString().ConvertStringCulture(DecimalSeparator) + "|" + value.ToString().ConvertStringCulture(DecimalSeparator); }
        }

        /// <summary>
        /// Get/Set Player DataItem[13]
        /// </summary>
        public string PokemonSkin { get; set; }

        /// <summary>
        /// Get/Set Player DataItem[14]
        /// </summary>
        public int PokemonFacing { get; set; }

        /// <summary>
        /// Get/Set Player Last Valid Game Data
        /// </summary>
        public List<string> LastValidGameData { get; set; } = new List<string>();

        #endregion Player Data

        public int ID { get; set; }

        public PlayerInfo PlayerInfo { get; private set; }
        public Networking Network { get; private set; }
        public DateTime LastValidMovement { get; set; } = DateTime.Now;
        public DateTime LastValidPing { get; set; } = DateTime.Now;

        private ThreadHelper Thread = new ThreadHelper();
        private DateTime JoinTime = DateTime.Now;

        public enum BusyTypes
        {
            NotBusy,
            Battling,
            Chatting,
            Inactive,
        }

        public enum OperatorTypes
        {
            Player,
            GameJoltPlayer,
            ChatModerator,
            ServerModerator,
            GlobalModerator,
            Administrator,
            Creator,
        }

        public Player(Core core, int id, Networking network, Package.Package package)
        {
            Core = core;
            ID = id;
            Network = network;
            Update(package, false);

            List<PlayerInfo> playerInfo = Core.SQLite.Query<PlayerInfo>(a => a.Name == Name && a.GameJoltID == GameJoltID);

            if (playerInfo.Count > 0)
                PlayerInfo = playerInfo[0];
            else
            {
                PlayerInfo = new PlayerInfo()
                {
                    Name = Name,
                    GameJoltID = GameJoltID,
                    IPAddress = Network.GetPublicIPFromClient()
                };

                Core.SQLite.Connection.Insert(PlayerInfo);
            }
        }

        public void Welcome()
        {
            Network.SentToPlayer(new Package.Package(Core, PackageTypes.ID, ID.ToString(), Network));
            UpdateWorld();

            List<Networking> activePlayer = Core.TcpClientCollection.ActivePlayer;

            foreach (Networking network in activePlayer)
            {
                if (network.Player.ID != ID)
                {
                    Network.SentToPlayer(new Package.Package(Core, PackageTypes.CreatePlayer, network.Player.ID.ToString(), Network));
                    Network.SentToPlayer(new Package.Package(Core, PackageTypes.GameData, network.Player.ID, network.Player.GenerateGameData(true), Network));
                }

                network.SentToPlayer(new Package.Package(Core, PackageTypes.CreatePlayer, ID.ToString(), Network));
                network.SentToPlayer(new Package.Package(Core, PackageTypes.GameData, GenerateGameData(true), Network));
                network.SentToPlayer(new Package.Package(Core, PackageTypes.ChatMessage, -1, IsGameJoltPlayer ?
                    Core.Settings.Tokens.ToString("SERVER_GAMEJOLT", Name, GameJoltID, "join the game!") :
                    Core.Settings.Tokens.ToString("SERVER_NOGAMEJOLT", Name, "join the game!"), Network));
            }

            Core.Logger.Log(IsGameJoltPlayer ?
                Core.Settings.Tokens.ToString("SERVER_GAMEJOLT", Name, GameJoltID, "join the game!") :
                Core.Settings.Tokens.ToString("SERVER_NOGAMEJOLT", Name, "join the game!"));

            if (!string.IsNullOrWhiteSpace(Core.Settings.Server.WelcomeMessage))
            {
                List<string> welcomeMessage = Core.Settings.Server.WelcomeMessage.Split('\n').ToList();

                foreach (string message in welcomeMessage)
                    Network.SentToPlayer(new Package.Package(Core, PackageTypes.ChatMessage, -1, message, Network));
            }

            if (Core.Settings.Features.AutoRestartTime >= 10)
            {
                TimeSpan timeLeft = Core.Listener.StartTime.AddSeconds(Core.Settings.Features.AutoRestartTime) - DateTime.Now;
                Network.SentToPlayer(new Package.Package(Core, PackageTypes.ChatMessage, -1, Core.Settings.Tokens.ToString("SERVER_RESTARTWARNING", timeLeft.ToString()), Network));
            }

            if (Core.Settings.Features.Chat.AllowChatChannel)
                Network.SentToPlayer(new Package.Package(Core, PackageTypes.ChatMessage, -1, Core.Settings.Tokens.ToString("SERVER_CURRENTCHATCHANNEL", "Default"), Network));

            CheckActivity();
        }

        public string GetPlayerBusyType()
        {
            switch (BusyType)
            {
                case (int)BusyTypes.NotBusy:
                    return "";

                case (int)BusyTypes.Battling:
                    return "- Battling";

                case (int)BusyTypes.Chatting:
                    return "- Chatting";

                case (int)BusyTypes.Inactive:
                    return "- Inactive";

                default:
                    return "";
            }
        }

        public void Update(Package.Package package, bool sentToAllPlayer)
        {
            LastValidMovement = DateTime.Now;

            if (package.IsFullPackageData())
            {
                GameMode = package.DataItems[0];
                IsGameJoltPlayer = package.DataItems[1].ToInt().ToBool();
                GameJoltID = IsGameJoltPlayer ? package.DataItems[2] : "-1";
                DecimalSeparator = package.DataItems[3];
                Name = package.DataItems[4];
                LevelFile = package.DataItems[5];
                Position = package.DataItems[6];
                Facing = package.DataItems[7].ToInt();
                Moving = package.DataItems[8].ToBool();
                Skin = package.DataItems[9];
                BusyType = package.DataItems[10].ToInt();
                PokemonVisible = package.DataItems[11].ToBool();
                PokemonPosition = package.DataItems[12];
                PokemonSkin = package.DataItems[13];
                PokemonFacing = package.DataItems[14].ToInt();

                LastValidGameData = new List<string> { LevelFile, Position, Facing.ToString(), Moving.ToString(), Skin, BusyType.ToString(), PokemonVisible.ToString(), PokemonPosition, PokemonSkin, PokemonFacing.ToString() };
            }
            else
            {
                LastValidGameData = new List<string> { LevelFile, Position, Facing.ToString(), Moving.ToString(), Skin, BusyType.ToString(), PokemonVisible.ToString(), PokemonPosition, PokemonSkin, PokemonFacing.ToString() };

                if (!string.IsNullOrWhiteSpace(package.DataItems[5]) && package.DataItems[5].SplitCount() == 1)
                    LevelFile = package.DataItems[5];
                if (!string.IsNullOrWhiteSpace(package.DataItems[6]) && package.DataItems[6].SplitCount() == 3)
                    Position = package.DataItems[6];
                if (!string.IsNullOrWhiteSpace(package.DataItems[7]) && package.DataItems[7].SplitCount() == 1)
                    Facing = package.DataItems[7].ToInt();
                if (!string.IsNullOrWhiteSpace(package.DataItems[8]) && package.DataItems[8].SplitCount() == 1)
                    Moving = package.DataItems[8].ToBool();
                if (!string.IsNullOrWhiteSpace(package.DataItems[9]) && package.DataItems[9].SplitCount() <= 2)
                    Skin = package.DataItems[9];
                if (!string.IsNullOrWhiteSpace(package.DataItems[10]) && package.DataItems[10].SplitCount() == 1)
                    BusyType = package.DataItems[10].ToInt();
                if (!string.IsNullOrWhiteSpace(package.DataItems[11]) && package.DataItems[11].SplitCount() == 1)
                    PokemonVisible = package.DataItems[11].ToBool();
                if (!string.IsNullOrWhiteSpace(package.DataItems[12]) && package.DataItems[12].SplitCount() == 3)
                    PokemonPosition = package.DataItems[12];
                if (!string.IsNullOrWhiteSpace(package.DataItems[13]) && package.DataItems[13].SplitCount() <= 2)
                    PokemonSkin = package.DataItems[13];
                if (!string.IsNullOrWhiteSpace(package.DataItems[14]) && package.DataItems[14].SplitCount() == 1)
                    PokemonFacing = package.DataItems[14].ToInt();
            }

            if (sentToAllPlayer)
            {
                Core.TcpClientCollection.SendToAllPlayer(new Package.Package(Core, PackageTypes.GameData, ID, GenerateGameData(package.IsFullPackageData()), Network));
            }
        }

        private List<string> GenerateGameData(bool fullPackageData)
        {
            List<string> ReturnList;

            if (fullPackageData)
            {
                ReturnList = new List<string>
                {
                    GameMode,
                    IsGameJoltPlayer.ToInt().ToString(),
                    IsGameJoltPlayer ? GameJoltID : "",
                    DecimalSeparator,
                    Name,
                    LevelFile,
                    Position.ConvertStringCulture(DecimalSeparator),
                    Facing.ToString(),
                    Moving.ToInt().ToString(),
                    Skin,
                    BusyType.ToString(),
                    PokemonVisible.ToInt().ToString(),
                    PokemonPosition.ConvertStringCulture(DecimalSeparator),
                    PokemonSkin,
                    PokemonFacing.ToString()
                };
            }
            else
            {
                ReturnList = new List<string>
                {
                    "",
                    "",
                    "",
                    "",
                    "",
                    LastValidGameData[0] == LevelFile ? "" : LevelFile,
                    LastValidGameData[1] == Position ? "" : Position.ConvertStringCulture(DecimalSeparator),
                    LastValidGameData[2] == Facing.ToString() ? "" : Facing.ToString(),
                    LastValidGameData[3] == Moving.ToString() ? "" : Moving.ToInt().ToString(),
                    LastValidGameData[4] == Skin ? "" : Skin,
                    LastValidGameData[5] == BusyType.ToString() ? "" : BusyType.ToString(),
                    LastValidGameData[6] == PokemonVisible.ToString() ? "" : PokemonVisible.ToInt().ToString(),
                    LastValidGameData[7] == PokemonPosition ? "" : PokemonPosition.ConvertStringCulture(DecimalSeparator),
                    LastValidGameData[8] == PokemonSkin ? "" : PokemonSkin,
                    LastValidGameData[9] == PokemonFacing.ToString() ? "" : PokemonFacing.ToString()
                };
            }

            return ReturnList;
        }

        private void CheckActivity()
        {
            Thread.Add(() =>
            {
                int hours = 0;

                do
                {
                    if (Core.Settings.Features.NoPingKickTime >= 10)
                    {
                        if ((DateTime.Now - LastValidPing).TotalSeconds >= Core.Settings.Features.NoPingKickTime)
                        {
                            InternalKick("SERVER_NOPING");
                            return;
                        }
                    }

                    if (Core.Settings.Features.AFKKickTime >= 10)
                    {
                        if ((DateTime.Now - LastValidMovement).TotalSeconds >= Core.Settings.Features.AFKKickTime && BusyType == (int)BusyTypes.Inactive)
                        {
                            InternalKick("SERVER_AFK");
                            return;
                        }
                    }

                    if (Core.Settings.Features.AutoRestartTime >= 10)
                    {
                        TimeSpan timeLeft = Core.Listener.StartTime.AddSeconds(Core.Settings.Features.AutoRestartTime) - DateTime.Now;

                        if (timeLeft.TotalSeconds <= 10)
                            Network.SentToPlayer(new Package.Package(Core, PackageTypes.ChatMessage, -1, Core.Settings.Tokens.ToString("SERVER_RESTARTWARNING", timeLeft.ToString()), Network));
                        else if (timeLeft.TotalSeconds <= 0)
                        {
                            InternalKick("SERVER_RESTART");
                            return;
                        }
                    }

                    if ((DateTime.Now - JoinTime).TotalHours >= hours + 1)
                    {
                        hours++;
                        Network.SentToPlayer(new Package.Package(Core, PackageTypes.ChatMessage, -1, Core.Settings.Tokens.ToString("SERVER_LOGINTIME", hours.ToString()), Network));
                    }

                    Thread.Sleep(1000);
                } while (Network.IsActive);
            });
        }

        private void InternalKick(string tokenKey, params string[] tokenValue)
        {
            Network.SentToPlayer(new Package.Package(Core, PackageTypes.Kicked, Core.Settings.Tokens.ToString(tokenKey, tokenValue), Network));
            Core.Logger.Log(IsGameJoltPlayer ?
                Core.Settings.Tokens.ToString("SERVER_GAMEJOLT", Name, GameJoltID, "have been kicked from the server with the following reason: " + Core.Settings.Tokens.ToString(tokenKey, tokenValue)) :
                Core.Settings.Tokens.ToString("SERVER_NOGAMEJOLT", Name, "have been kicked from the server with the following reason: " + Core.Settings.Tokens.ToString(tokenKey, tokenValue)));
        }

        public override string ToString()
        {
            return $"{(IsGameJoltPlayer ? $"{Name} ({GameJoltID})" : Name)} {GetPlayerBusyType()}";
        }

        public void Dispose()
        {
            Thread.Dispose();

            Core.BaseInstance.PlayerList.Update(new PlayerListEventArgs(Network, Operations.Remove, ID));

            Core.TcpClientCollection.SendToAllPlayer(new Package.Package(Core, PackageTypes.DestroyPlayer, ID.ToString(), Network));
            Core.TcpClientCollection.SendToAllPlayer(new Package.Package(Core, PackageTypes.ChatMessage, -1, IsGameJoltPlayer ?
                    Core.Settings.Tokens.ToString("SERVER_GAMEJOLT", Name, GameJoltID, "left the server.") :
                    Core.Settings.Tokens.ToString("SERVER_NOGAMEJOLT", Name, "left the server."), Network));

            Core.Logger.Log(IsGameJoltPlayer ?
                Core.Settings.Tokens.ToString("SERVER_GAMEJOLT", Name, GameJoltID, "left the server.") :
                Core.Settings.Tokens.ToString("SERVER_NOGAMEJOLT", Name, "left the server."));

            lock (Core.SQLite.Connection)
            {
                PlayerInfo.LastActivity = DateTime.Now;

                if (IsGameJoltPlayer)
                    Core.SQLite.Connection.Update(PlayerInfo);
                else
                    Core.SQLite.Connection.Delete(PlayerInfo);
            }
        }
    }
}
