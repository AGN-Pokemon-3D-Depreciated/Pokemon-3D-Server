using SQLite;

namespace Pokemon_3D_Server_Core.SQLite.Models
{
    public class WhiteList
    {
        /// <summary>
        /// Get/Set WhiteList ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Get/Set WhiteList Player ID.
        /// </summary>
        [NotNull]
        public int PlayerID { get; set; }

        /// <summary>
        /// Get/Set WhiteList Reason.
        /// </summary>
        public string Reason { get; set; }
    }
}
