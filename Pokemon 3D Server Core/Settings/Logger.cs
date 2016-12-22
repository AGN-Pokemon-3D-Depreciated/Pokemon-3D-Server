namespace Pokemon_3D_Server_Core.Settings
{
    public class Logger
    {
        /// <summary>
        /// Message Log Type.
        /// </summary>
        public enum LogTypes
        {
            /// <summary>
            /// General Log Type.
            /// </summary>
            Info,

            /// <summary>
            /// Warning Log Type.
            /// </summary>
            Warning,

            /// <summary>
            /// Error Log Type.
            /// </summary>
            Error,

            /// <summary>
            /// Debug Log Type.
            /// </summary>
            Debug,

            /// <summary>
            /// Chat Log Type.
            /// </summary>
            Chat,

            /// <summary>
            /// PM Log Type.
            /// </summary>
            PM,

            /// <summary>
            /// Server Chat Log Type.
            /// </summary>
            Server,

            /// <summary>
            /// Trade Log Type.
            /// </summary>
            Trade,

            /// <summary>
            /// PvP Log Type.
            /// </summary>
            PvP,

            /// <summary>
            /// Command Log Type.
            /// </summary>
            Command,

            /// <summary>
            /// Rcon Log Type.
            /// </summary>
            Rcon,
        }

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
