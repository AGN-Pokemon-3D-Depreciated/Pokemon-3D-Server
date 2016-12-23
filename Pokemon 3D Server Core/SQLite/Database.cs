using Pokemon_3D_Server_Core.Interface;
using Pokemon_3D_Server_Core.SQLite.Models;
using SQLite;
using System.Collections.Generic;

namespace Pokemon_3D_Server_Core.SQLite
{
    public class Database : IModules
    {
        private SQLiteConnection Connection;

        /// <summary>
        /// Get List of PlayerInfo.
        /// </summary>
        public List<PlayerInfo> PlayerInfo { get; private set; } = new List<PlayerInfo>();

        /// <summary>
        /// Get List of BlackList.
        /// </summary>
        public List<BlackList> BlackList { get; private set; } = new List<BlackList>();

        /// <summary>
        /// Get List of IP BlackList.
        /// </summary>
        public List<IPBlackList> IPBlackList { get; private set; } = new List<IPBlackList>();

        /// <summary>
        /// Get List of MuteList.
        /// </summary>
        public List<MuteList> MuteList { get; private set; } = new List<MuteList>();

        /// <summary>
        /// Get List of OperatorList.
        /// </summary>
        public List<OperatorList> OperatorList { get; private set; } = new List<OperatorList>();

        /// <summary>
        /// Get List of WhiteList.
        /// </summary>
        public List<WhiteList> WhiteList { get; private set; } = new List<WhiteList>();

        /// <summary>
        /// Get List of Chat History.
        /// </summary>
        public List<ChatHistoryList> ChatHistoryList { get; private set; } = new List<ChatHistoryList>();

        /// <summary>
        /// Get List of Trade History.
        /// </summary>
        public List<TradeHistoryList> TradeHistoryList { get; private set; } = new List<TradeHistoryList>();

        /// <summary>
        /// Get the name of the module.
        /// </summary>
        public string Name { get { return "SQLite Database"; } }

        /// <summary>
        /// Get the version of the module.
        /// </summary>
        public string Version { get { return "0.54"; } }

        public void Start()
        {
            Connection = new SQLiteConnection($"{Core.Settings.Directories.DataDirectory}/{Core.Settings.Server.Features.Sqlite.Name}.db", true);
            Connection.CreateTable<PlayerInfo>(CreateFlags.AutoIncPK);
            Connection.CreateTable<BlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<IPBlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<MuteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<OperatorList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<WhiteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<ChatHistoryList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<TradeHistoryList>(CreateFlags.AutoIncPK);

            PlayerInfo = Connection.Query<PlayerInfo>("select * from PlayerInfo");
            BlackList = Connection.Query<BlackList>("select * from BlackList");
            IPBlackList = Connection.Query<IPBlackList>("select * from IPBlackList");
            MuteList = Connection.Query<MuteList>("select * from MuteList");
            OperatorList = Connection.Query<OperatorList>("select * from OperatorList");
            WhiteList = Connection.Query<WhiteList>("select * from WhiteList");
            ChatHistoryList = Connection.Query<ChatHistoryList>("select * from ChatHistoryList");
            TradeHistoryList = Connection.Query<TradeHistoryList>("select * from TradeHistoryList");
        }

        public void Stop()
        {
            Connection.Close();
        }
    }
}
