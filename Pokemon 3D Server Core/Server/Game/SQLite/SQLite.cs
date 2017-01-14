using Modules.System.IO;
using Pokemon_3D_Server_Core.Interface;
using Pokemon_3D_Server_Core.Server.Game.SQLite.Tables;
using SQLite;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite
{
    public class SQLite : IModules
    {
        public string Name { get; } = "SQLite Database";
        public string Version { get; } = "0.54";

        public SQLiteConnection Connection { get; private set; }

        public void Start()
        {
            Connection = new SQLiteConnection($"{Core.Settings.Directories.DataDirectory}/{Core.Settings.Server.Game.Features.SQLite.DatabaseName}.db".GetFullPath(), true);
            Connection.CreateTable<PlayerInfo>(CreateFlags.AutoIncPK);
            Connection.CreateTable<BlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<IPBlackList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<MuteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<OperatorList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<WhiteList>(CreateFlags.AutoIncPK);
            Connection.CreateTable<TradeHistoryList>(CreateFlags.AutoIncPK);
        }

        public void Stop()
        {
            Connection.Close();
        }
    }
}
