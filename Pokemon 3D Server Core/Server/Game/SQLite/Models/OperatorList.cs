using SQLite;

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

        /// <summary>
        /// A Collection of Operator Type
        /// </summary>
        public enum OperatorTypes
        {
            /// <summary>
            /// Normal Player
            /// </summary>
            Player = 0,

            /// <summary>
            /// GameJolt Player
            /// </summary>
            GameJoltPlayer = 1,

            /// <summary>
            /// Player with Chat Moderator ability
            /// </summary>
            ChatModerator = 2,

            /// <summary>
            /// Player with Server Moderator ability
            /// </summary>
            ServerModerator = 3,

            /// <summary>
            /// Player with Global Moderator ability
            /// </summary>
            GlobalModerator = 4,

            /// <summary>
            /// Player with Administrator ability
            /// </summary>
            Administrator = 5,

            /// <summary>
            /// Player with Administrator ability and Debugging ability
            /// </summary>
            Creator = 6,
        }
    }
}
