using SQLite;
using static Pokemon_3D_Server_Core.Collections.OperatorTypeCollection;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite.Models
{
    public class OperatorList
    {
        /// <summary>
        /// Get/Set OperatorList ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Get/Set OperatorList Player ID.
        /// </summary>
        [NotNull]
        public int PlayerID { get; set; }

        /// <summary>
        /// Get/Set OperatorList Reason.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Get/Set OperatorList Permission.
        /// </summary>
        [NotNull]
        public OperatorTypes Permission { get; set; }
    }
}
