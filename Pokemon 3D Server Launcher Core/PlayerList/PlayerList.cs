using Amib.Threading;
using System;

namespace Pokemon_3D_Server_Launcher_Core.PlayerList
{
    public sealed class PlayerList
    {
        private Core Core;
        private IWorkItemsGroup ThreadPool = new SmartThreadPool().CreateWorkItemsGroup(1, new WIGStartInfo() { StartSuspended = true });
        private bool IsActive = false;

        public event EventHandler<PlayerListEventArgs> OnPlayerListUpdate;

        internal PlayerList(Core core)
        {
            Core = core;
            IsActive = false;
            Core.Logger.Log("PlayerList Initialized.", "Info");
        }

        internal void Start()
        {
            IsActive = true;
            ThreadPool.Start();
        }

        public void Update(PlayerListEventArgs playerListEventArgs)
        {
            ThreadPool.QueueWorkItem(() =>
            {
                if (IsActive)
                    InternalUpdate(playerListEventArgs);
            });
        }

        private void InternalUpdate(PlayerListEventArgs playerListEventArgs)
        {
            OnPlayerListUpdate.BeginInvoke(this, playerListEventArgs, null, null);
        }

        internal void Dispose()
        {
            if (IsActive)
            {
                IsActive = false;
                ThreadPool.WaitForIdle();
            }
        }
    }
}
