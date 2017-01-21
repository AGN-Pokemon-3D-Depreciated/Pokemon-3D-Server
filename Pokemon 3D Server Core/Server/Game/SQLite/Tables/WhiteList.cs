using SQLite;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite.Tables
{
    public class WhiteList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int PlayerID { get; set; }

        public string Reason { get; set; }

        public static string GetReason(int playerID)
        {
            lock (Core.SQLite.Connection)
            {
                TableQuery<WhiteList> stm = Core.SQLite.Connection.Table<WhiteList>().Where(a => a.PlayerID == playerID);

                if (stm.Count() > 0)
                {
                    WhiteList whiteList = stm.First();
                    return whiteList.Reason;
                }
            }

            return null;
        }

        public static bool Exists(int playerID)
        {
            lock (Core.SQLite.Connection)
            {
                TableQuery<WhiteList> stm = Core.SQLite.Connection.Table<WhiteList>().Where(a => a.PlayerID == playerID);

                if (stm.Count() > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}