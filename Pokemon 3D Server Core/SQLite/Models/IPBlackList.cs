using SQLite;
using System;

namespace Pokemon_3D_Server_Core.SQLite.Models
{
    public class IPBlackList
    {
        /// <summary>
        /// Get/Set IPBlackList ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Get/Set IPBlackList Player ID.
        /// </summary>
        [NotNull]
        public int PlayerID { get; set; }

        /// <summary>
        /// Get/Set IPBlackList Reason.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Get/Set IPBlackList StartTime.
        /// </summary>
        [NotNull]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Get/Set IPBlackList Duration.
        /// </summary>
        [NotNull]
        public long Duration { get; set; }
    }
}
