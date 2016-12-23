using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_3D_Server_Core.SQLite.Models
{
    public class TradeHistoryList
    {
        /// <summary>
        /// Get/Set Trade ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Get/Set Host ID.
        /// </summary>
        [NotNull]
        public int HostID { get; set; }

        /// <summary>
        /// Get/Set Host Pokemon.
        /// </summary>
        [NotNull]
        public string HostPokemon { get; set; }

        /// <summary>
        /// Get/Set Client ID.
        /// </summary>
        [NotNull]
        public int ClientID { get; set; }

        /// <summary>
        /// Get/Set Client Pokemon.
        /// </summary>
        [NotNull]
        public string ClientPokemon { get; set; }

        /// <summary>
        /// Get/Set TimeStamp.
        /// </summary>
        [NotNull]
        public DateTime TimeStamp { get; set; }
    }
}
