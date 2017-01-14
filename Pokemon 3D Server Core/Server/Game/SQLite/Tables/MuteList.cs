using SQLite;
using System;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite.Tables
{
    public class MuteList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int PlayerID { get; set; } = -1;

        [NotNull]
        public int MuteID { get; set; }

        public string Reason { get; set; }

        [NotNull]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [NotNull]
        public long Duration { get; set; } = -1;
    }
}
