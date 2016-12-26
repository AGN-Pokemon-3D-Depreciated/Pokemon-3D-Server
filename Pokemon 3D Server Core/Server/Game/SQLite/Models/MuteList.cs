using SQLite;
using System;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite.Models
{
    public class MuteList
    {
        /// <summary>
        /// Get/Set MuteList ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Get/Set MuteList Player ID.
        /// </summary>
        [NotNull]
        public int PlayerID { get; set; } = -1;

        /// <summary>
        /// Get/Set MuteList Player ID.
        /// </summary>
        [NotNull]
        public int MuteID { get; set; }

        /// <summary>
        /// Get/Set MuteList Reason.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Get/Set MuteList StartTime.
        /// </summary>
        [NotNull]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Get/Set MuteList Duration.
        /// </summary>
        [NotNull]
        public long Duration { get; set; }
    }
}
