namespace Pokemon_3D_Server_Core.Collections
{
    public class LogTypeCollection
    {
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
    }
}
