using SQLite;
using System;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Features
{
    public class ChatHistoryList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int PlayerID { get; set; }

        [NotNull]
        public int PMPlayerID { get; set; } = -1;

        [NotNull]
        public string Message { get; set; }

        [NotNull]
        public DateTime TimeStamp { get; set; }
    }
}
