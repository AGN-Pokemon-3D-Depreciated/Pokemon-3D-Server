namespace Pokemon_3D_Server_Core.Settings.Server.Features
{
    public class Features
    {
        /// <summary>
        /// Get/Set BlackList Feature.
        /// </summary>
        public bool BlackList { get; set; } = true;

        /// <summary>
        /// Get/Set IPBlackList Feature.
        /// </summary>
        public bool IPBlackList { get; set; } = true;

        /// <summary>
        /// Get/Set WhiteList Feature.
        /// </summary>
        public bool WhiteList { get; set; } = false;

        /// <summary>
        /// Get/Set OperatorList Feature.
        /// </summary>
        public bool OperatorList { get; set; } = true;

        /// <summary>
        /// Get/Set MuteList Feature.
        /// </summary>
        public bool MuteList { get; set; } = true;

        /// <summary>
        /// Get Sqlite Feature.
        /// </summary>
        public Sqlite Sqlite { get; private set; } = new Sqlite();
    }
}
