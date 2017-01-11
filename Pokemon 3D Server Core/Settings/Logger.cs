namespace Pokemon_3D_Server_Core.Settings
{
    public class Logger
    {
        /// <summary>
        /// Get/Set Logger Info.
        /// </summary>
        public bool LoggerInfo { get; set; } = true;

        /// <summary>
        /// Get/Set Logger Warning.
        /// </summary>
        public bool LoggerWarning { get; set; } = true;

        /// <summary>
        /// Get/Set Logger Error.
        /// </summary>
        public bool LoggerError { get; set; } = true;

        /// <summary>
        /// Get/Set Logger Debug.
        /// </summary>
        public bool LoggerDebug { get; set; } = false;

        /// <summary>
        /// Get/Set Logger Chat.
        /// </summary>
        public bool LoggerChat { get; set; } = true;

        /// <summary>
        /// Get/Set Logger PM.
        /// </summary>
        public bool LoggerPM { get; set; } = true;

        /// <summary>
        /// Get/Set Logger Server.
        /// </summary>
        public bool LoggerServer { get; set; } = true;

        /// <summary>
        /// Get/Set Logger Trade.
        /// </summary>
        public bool LoggerTrade { get; set; } = true;

        /// <summary>
        /// Get/Set Logger PvP.
        /// </summary>
        public bool LoggerPvP { get; set; } = true;

        /// <summary>
        /// Get/Set Logger Command.
        /// </summary>
        public bool LoggerCommand { get; set; } = true;

        /// <summary>
        /// Get/Set Logger Rcon.
        /// </summary>
        public bool LoggerRcon { get; set; } = true;
    }
}
