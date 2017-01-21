using SQLite;
using System;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite.Tables
{
    public class IPBlackList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public string IPAddress { get; set; }

        public string Reason { get; set; }

        [NotNull]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [NotNull]
        public long Duration { get; set; } = -1;

        public static string GetReason(string ipAddress)
        {
            Update();

            lock (Core.SQLite.Connection)
            {
                TableQuery<IPBlackList> stm = Core.SQLite.Connection.Table<IPBlackList>().Where(a => a.IPAddress == ipAddress);

                if (stm.Count() > 0)
                {
                    IPBlackList ipBlacklist = stm.First();
                    return ipBlacklist.Reason;
                }
            }

            return null;
        }

        public static string GetDuration(string ipAddress)
        {
            Update();

            lock (Core.SQLite.Connection)
            {
                TableQuery<IPBlackList> stm = Core.SQLite.Connection.Table<IPBlackList>().Where(a => a.IPAddress == ipAddress);

                if (stm.Count() > 0)
                {
                    IPBlackList ipBlacklist = stm.First();

                    if (ipBlacklist.Duration != -1)
                    {
                        if (DateTime.Now < ipBlacklist.StartTime.AddSeconds(ipBlacklist.Duration))
                        {
                            TimeSpan duration = ipBlacklist.StartTime.AddSeconds(ipBlacklist.Duration) - DateTime.Now;
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
                TableQuery<IPBlackList> stm = Core.SQLite.Connection.Table<IPBlackList>();

                if (stm.Count() > 0)
                {
                    for (int i = 0; i < stm.Count(); i++)
                    {
                        IPBlackList ipBlacklist = stm.ElementAt(i);

                        if (ipBlacklist.Duration != -1)
                        {
                            if (DateTime.Now >= ipBlacklist.StartTime.AddSeconds(ipBlacklist.Duration))
                                Core.SQLite.Connection.Delete(ipBlacklist);
                        }
                    }
                }
            }
        }

        public static bool Exists(string ipAddress)
        {
            Update();

            lock (Core.SQLite.Connection)
            {
                TableQuery<IPBlackList> stm = Core.SQLite.Connection.Table<IPBlackList>().Where(a => a.IPAddress == ipAddress);

                if (stm.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool IsBanned(string ipAddress)
        {
            Update();

            lock (Core.SQLite.Connection)
            {
                TableQuery<IPBlackList> stm = Core.SQLite.Connection.Table<IPBlackList>().Where(a => a.IPAddress == ipAddress);

                if (stm.Count() > 0)
                {
                    IPBlackList ipBlacklist = stm.First();

                    if (ipBlacklist.Duration != -1)
                    {
                        if (DateTime.Now < ipBlacklist.StartTime.AddSeconds(ipBlacklist.Duration))
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