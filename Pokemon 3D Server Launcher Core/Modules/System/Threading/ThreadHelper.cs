using System;
using System.Collections.Generic;
using System.Threading;

namespace Pokemon_3D_Server_Launcher_Core.Modules.System.Threading
{
    public class ThreadHelper : List<Thread>, IDisposable
    {
        public void Add(ThreadStart threadStart)
        {
            Thread thread = new Thread(threadStart) { IsBackground = true };
            thread.Start();
            Add(thread);
        }

        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        public bool IsActive()
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].IsAlive)
                    return true;
            }

            return false;
        }

        public void Dispose()
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].IsAlive)
                    this[i].Abort();
            }

            Clear();
        }
    }
}
