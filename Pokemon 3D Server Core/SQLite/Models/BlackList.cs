using SQLite;
using System;

namespace Pokemon_3D_Server_Core.SQLite.Models
{
    public class BlackList
    {
        /// <summary>
        /// Get/Set BlackList ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Get/Set BlackList Player ID.
        /// </summary>
        [NotNull]
        public int PlayerID { get; set; }

        /// <summary>
        /// Get/Set BlackList Reason.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Get/Set BlackList StartTime.
        /// </summary>
        [NotNull]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Get/Set BlackList Duration.
        /// </summary>
        [NotNull]
        public long Duration { get; set; }
    }
}
