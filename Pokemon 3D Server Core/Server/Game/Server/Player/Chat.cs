using System;

namespace Pokemon_3D_Server_Core.Server.Game.Server.Player
{
    public partial class Player
    {
        public DateTime LastChatTime { get; set; } = DateTime.Now;
        public int CurrentChatChannel { get; set; } = 0;
    }
}
