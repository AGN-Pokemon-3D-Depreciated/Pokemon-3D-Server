using SQLite;
using System;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Features
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
        public DateTime StartTime { get; set; }

        [NotNull]
        public long Duration { get; set; }
    }
}
