using System;

namespace Pokemon_3D_Server_Core.Server
{
    public class TcpClientCollection : IDisposable
    {
        public Game.Server.TcpClientCollection GameTcpClientCollection { get; private set; } = new Game.Server.TcpClientCollection();

        #region IDisposable Support

        private bool disposedValue = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "<GameTcpClientCollection>k__BackingField")]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GameTcpClientCollection.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}