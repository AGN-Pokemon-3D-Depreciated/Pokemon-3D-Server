using SQLite;
using System;

namespace Pokemon_3D_Server_Launcher_Game_Module.SQLite.Tables
{
    public class TradeHistoryList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int HostID { get; set; }

        [NotNull]
        public string HostPokemon { get; set; }

        [NotNull]
        public int ClientID { get; set; }

        [NotNull]
        public string ClientPokemon { get; set; }

        [NotNull]
        public DateTime TimeStamp { get; set; }
    }
}
