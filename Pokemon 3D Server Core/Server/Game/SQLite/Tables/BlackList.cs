using SQLite;
using System;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite.Tables
{
    public class BlackList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int PlayerID { get; set; }

        public string Reason { get; set; }

        [NotNull]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [NotNull]
        public long Duration { get; set; } = -1;

        public static string GetReason(int playerID)
        {
            Update();

            lock (Core.SQLite.Connection)
            {
                TableQuery<BlackList> stm = Core.SQLite.Connection.Table<BlackList>().Where(a => a.PlayerID == playerID);

                if (stm.Count() > 0)
                {
                    BlackList blacklist = stm.First();
                    return blacklist.Reason;
                }
            }

            return null;
        }

        public static string GetDuration(int playerID)
        {
            Update();

            lock (Core.SQLite.Connection)
            {
                TableQuery<BlackList> stm = Core.SQLite.Connection.Table<BlackList>().Where(a => a.PlayerID == playerID);

                if (stm.Count() > 0)
                {
                    BlackList blacklist = stm.First();

                    if (blacklist.Duration != -1)
                    {
                        if (DateTime.Now < blacklist.StartTime.AddSeconds(blacklist.Duration))
                        {
                            TimeSpan duration = blacklist.StartTime.AddSeconds(blacklist.Duration) - DateTime.Now;
                            return duration.ToString();
                        }
                    }
                    else
                        return "Permanent";
                }
            }

            return null;
        }

        public static void Update()
        {
            lock (Core.SQLite.Connection)
            {
                TableQuery<BlackList> stm = Core.SQLite.Connection.Table<BlackList>();

                if (stm.Count() > 0)
                {
                    for (int i = 0; i < stm.Count(); i++)
                    {
                        BlackList blacklist = stm.ElementAt(i);

                        if (blacklist.Duration != -1)
                        {
                            if (DateTime.Now >= blacklist.StartTime.AddSeconds(blacklist.Duration))
                                Core.SQLite.Connection.Delete(blacklist);
                        }
                    }
                }
            }
        }

        public static bool Exists(int playerID)
        {
            Update();

            lock (Core.SQLite.Connection)
            {
                TableQuery<BlackList> stm = Core.SQLite.Connection.Table<BlackList>().Where(a => a.PlayerID == playerID);

                if (stm.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool IsBanned(int playerID)
        {
            Update();

            lock (Core.SQLite.Connection)
            {
                TableQuery<BlackList> stm = Core.SQLite.Connection.Table<BlackList>().Where(a => a.PlayerID == playerID);

                if (stm.Count() > 0)
                {
                    BlackList blacklist = stm.First();

                    if (blacklist.Duration != -1)
                    {
                        if (DateTime.Now < blacklist.StartTime.AddSeconds(blacklist.Duration))
                            return true;
                    }
                    else
                        return true;
                }
            }

            return false;
        }
    }
}