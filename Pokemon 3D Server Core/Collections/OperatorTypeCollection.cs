namespace Pokemon_3D_Server_Core.Collections
{
    public class OperatorTypeCollection
    {
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
