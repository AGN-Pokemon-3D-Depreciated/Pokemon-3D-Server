using Modules.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon_3D_Server_Core.Server.Game.Server.Package
{
    public class Package
    {
        private string ProtocolVersion = Core.Settings.Server.Game.Network.ProtocolVersion;
        private PackageHandler PackageHandler;

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

        public Package(string fullData, Networking network)
        {
            try
            {
                Network = network;

                if (fullData == null || !fullData.Contains("|"))
                {
                    Core.Logger.Debug($"Package is incomplete.", network);
                    IsValid = false;
                    return;
                }

                List<string> bits = fullData.Split('|').ToList();

                if (bits.Count >= 5)
                {
                    // Protocol Version
                    if (!string.Equals(Core.Settings.Server.Game.Network.ProtocolVersion, bits[0], StringComparison.OrdinalIgnoreCase))
                    {
                        Core.Logger.Debug($"Package does not contains valid Protocol Version.", network);
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
                        Core.Logger.Debug($"Package does not contains valid Package Type.", network);
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
                        Core.Logger.Debug($"Package does not contains valid Origin.", network);
                        IsValid = false;
                        return;
                    }

                    // DataItemsCount
                    int DataItemsCount = 0;

                    try
                    {
                        DataItemsCount = bits[3].ToInt();
                    }
                    catch (Exception)
                    {
                        Core.Logger.Debug($"Package does not contains valid DataItemsCount.", network);
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
                            Core.Logger.Debug($"Package does not contains valid Offset.", network);
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
                    Core.Logger.Debug($"Package is incomplete.", network);
                    IsValid = false;
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Debug(ex.Message, network);
                IsValid = false;
            }
        }

        public Package(PackageTypes packageType, int origin, List<string> dataItems, Networking network = null)
        {
            PackageType = (int)packageType;
            Origin = origin;
            DataItems = dataItems;
            Network = network;
            IsValid = true;
        }

        public Package(PackageTypes packageType, List<string> dataItems, Networking network = null)
        {
            PackageType = (int)packageType;
            DataItems = dataItems;
            Network = network;
            IsValid = true;
        }

        public Package(PackageTypes packageType, int origin, string dataItems, Networking network = null)
        {
            PackageType = (int)packageType;
            Origin = origin;
            DataItems = new List<string> { dataItems };
            Network = network;
            IsValid = true;
        }

        public Package(PackageTypes packageType, string dataItems, Networking network = null)
        {
            PackageType = (int)packageType;
            DataItems = new List<string> { dataItems };
            Network = network;
            IsValid = true;
        }

        public void Handle()
        {
            PackageHandler = new PackageHandler(this);
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
            string outputStr = ProtocolVersion + "|" + PackageType.ToString() + "|" + Origin.ToString() + "|" + DataItemsCount.ToString();

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
    }
}
