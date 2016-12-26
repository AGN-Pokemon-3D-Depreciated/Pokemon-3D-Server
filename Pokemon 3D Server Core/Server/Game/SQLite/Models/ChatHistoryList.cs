using SQLite;
using System;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite.Models
{
    public class ChatHistoryList
    {
        /// <summary>
        /// Get/Set Chat History ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Get/Set Chat History Player ID.
        /// </summary>
        [NotNull]
        public int PlayerID { get; set; }

        /// <summary>
        /// Get/Set Chat History Player ID.
        /// </summary>
        [NotNull]
        public int PMPlayerID { get; set; } = -1;

        /// <summary>
        /// Get/Set Chat History Message.
        /// </summary>
        [NotNull]
        public string Message { get; set; }

        /// <summary>
        /// Get/Set Chat History TimeStamp.
        /// </summary>
        [NotNull]
        public DateTime TimeStamp { get; set; }
    }
}
