using System;
using System.Collections.Generic;
using System.Threading;

namespace Modules.System.Threading
{
    /// <summary>
    /// Class containing Thread Collection.
    /// </summary>
    public class ThreadHelper : List<Thread>, IDisposable
    {
        /// <summary>
        /// Add a new thread into the collection.
        /// </summary>
        /// <param name="threadStart">Thread to add.</param>
        public void Add(ThreadStart threadStart)
        {
            Thread thread = new Thread(threadStart) { IsBackground = true };
            thread.Start();
            Add(thread);
        }

        /// <summary>
        /// Suspends the current thread for the specified number of milliseconds.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds for which the thread is suspended. If the value of the millisecondsTimeout argument is zero, the thread relinquishes the remainder of its time slice to any thread of equal priority that is ready to run. If there are no other threads of equal priority that are ready to run, execution of the current thread is not suspended.</param>
        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        public bool isActive()
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].IsAlive)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Dispose all running threads.
        /// </summary>
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
