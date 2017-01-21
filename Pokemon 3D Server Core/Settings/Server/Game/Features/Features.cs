namespace Pokemon_3D_Server_Core.Settings.Server.Game.Features
{
    public class Features
    {
        public bool PlayerInfo { get; set; } = true;
        public bool BlackList { get; set; } = true;
        public bool IPBlackList { get; set; } = true;
        public bool MuteList { get; set; } = true;
        public bool OperatorList { get; set; } = true;
        public bool WhiteList { get; set; } = false;
        public bool TradeHistory { get; set; } = true;

        private int _NoPingKickTime = 30;

        public int NoPingKickTime
        {
            get
            {
                return _NoPingKickTime;
            }
            set
            {
                if (value < 10)
                    _NoPingKickTime = -1;
                else
                    _NoPingKickTime = value;
            }
        }

        private int _AFKKickTime = 300;

        public int AFKKickTime
        {
            get
            {
                return _AFKKickTime;
            }
            set
            {
                if (value < 10)
                    _AFKKickTime = -1;
                else
                    _AFKKickTime = value;
            }
        }

        private int _AutoRestartTime = -1;

        public int AutoRestartTime
        {
            get
            {
                return _AutoRestartTime;
            }
            set
            {
                if (value < 10)
                    _AutoRestartTime = -1;
                else
                    _AutoRestartTime = value;
            }
        }

        public Chat.Chat Chat { get; private set; } = new Chat.Chat();
        public SQLite.SQLite SQLite { get; private set; } = new SQLite.SQLite();
    }
}