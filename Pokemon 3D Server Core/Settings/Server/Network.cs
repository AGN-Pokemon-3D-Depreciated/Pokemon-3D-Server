namespace Pokemon_3D_Server_Core.Settings.Server
{
    public class Network
    {
        private int _NoPingKickTime = 30;
        /// <summary>
        /// Get/Set No Ping Kick Time
        /// </summary>
        public int NoPingKickTime
        {
            get { return _NoPingKickTime; }
            set
            {
                if (value < 10)
                    _NoPingKickTime = -1;
                else
                    _NoPingKickTime = value;
            }
        }

        private int _AFKKickTime = 300;
        /// <summary>
        /// Get/Set AFK Kick Time
        /// </summary>
        public int AFKKickTime
        {
            get { return _AFKKickTime; }
            set
            {
                if (value < 10)
                    _AFKKickTime = -1;
                else
                    _AFKKickTime = value;
            }
        }

        private int _AutoRestartTime = -1;
        /// <summary>
        /// Get/Set Auto Restart Time
        /// </summary>
        public int AutoRestartTime
        {
            get { return _AutoRestartTime; }
            set
            {
                if (value < 10)
                    _AutoRestartTime = -1;
                else
                    _AutoRestartTime = value;
            }
        }
    }
}
