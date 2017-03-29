using System;

namespace Pokemon_3D_Server_Launcher_Core.PlayerList
{
    public sealed class PlayerList
    {
        private Core Core;
        private bool IsActive = false;

        public event EventHandler<PlayerListEventArgs> OnPlayerListUpdate;

        internal PlayerList(Core core)
        {
            Core = core;
            IsActive = false;
            Core.Logger.Log("PlayerList Initialized.");
        }

        internal void Start()
        {
            IsActive = true;
        }

        public void Update(PlayerListEventArgs playerListEventArgs)
        {
            if (IsActive)
                OnPlayerListUpdate.BeginInvoke(this, playerListEventArgs, null, null);
        }

        internal void Dispose()
        {
            if (IsActive)
            {
                IsActive = false;
                Core.Logger.Log("PlayerList Disposed.");
            }
        }
    }
}
