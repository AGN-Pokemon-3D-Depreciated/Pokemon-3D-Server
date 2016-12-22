using System;

namespace Pokemon_3D_Server_Core.Interface
{
    public interface IModules
    {
        /// <summary>
        /// Get the name of the module.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get the version of the module.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Start the module.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the module.
        /// </summary>
        void Stop();
    }
}
