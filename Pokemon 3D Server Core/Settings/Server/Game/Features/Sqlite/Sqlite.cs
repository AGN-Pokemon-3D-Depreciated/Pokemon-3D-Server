using Modules.System.IO;
using SQLite;
using YamlDotNet.Serialization;
using System.Linq;
using System;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Features.Sqlite
{
    public class Sqlite
    {
        [YamlMember(Alias = "Database Name")]
        public string DatabaseName { get; set; } = "Pokemon_3D_Data";

        [YamlIgnore]
        private SQLiteConnection Connection;

        public Sqlite()
        {
            Connection = new SQLiteConnection($"{Core.Settings.Directories.DataDirectory}/{DatabaseName}.db".GetFullPath(), true);
            Connection.CreateTable<PlayerInfo>(CreateFlags.AutoIncPK);
            Connection.CreateTable<BlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<IPBlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<MuteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<OperatorList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<WhiteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<ChatHistoryList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<TradeHistoryList>(CreateFlags.AutoIncPK);
        }

        ~Sqlite()
        {
            Connection.Close();
        }

        #region PlayerInfo
        public int GetPlayerID(string name, string gamejoltID)
        {
            try
            {
                if (gamejoltID != "-1")
                    return Connection.Table<PlayerInfo>().Where(a => a.GameJoltID == gamejoltID).FirstOrDefault().ID;
                else
                    return Connection.Table<PlayerInfo>().Where(a => a.Name == name && a.GameJoltID == gamejoltID).FirstOrDefault().ID;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion PlayerInfo
    }
}
