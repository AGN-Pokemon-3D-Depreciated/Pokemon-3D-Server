using SQLite;
using System;

namespace Pokemon_3D_Server_Launcher_Game_Module.SQLite.Tables
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
    }
}
