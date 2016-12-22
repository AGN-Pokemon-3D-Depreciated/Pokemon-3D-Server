using System;
using Modules.System.IO;

namespace Pokemon_3D_Server_Core.Settings
{
    public class Directories
    {
        /// <summary>
        /// Get Application Directory.
        /// </summary>
        public string ApplicationDirectory { get; } = AppDomain.CurrentDomain.BaseDirectory.GetFullPath();

        /// <summary>
        /// Get Crash Log Directory.
        /// </summary>
        public string CrashLogDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/CrashLogs".GetFullPath());
                return $"{ApplicationDirectory}/CrashLogs".GetFullPath();
            }
        }

        /// <summary>
        /// Get Data Directory.
        /// </summary>
        public string DataDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/Data".GetFullPath());
                return $"{ApplicationDirectory}/Data".GetFullPath();
            }
        }

        /// <summary>
        /// Get Logger Directory.
        /// </summary>
        public string LoggerDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/Logger".GetFullPath());
                return $"{ApplicationDirectory}/Logger".GetFullPath();
            }
        }

        /// <summary>
        /// Get Download Directory.
        /// </summary>
        public string DownloadDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/Download".GetFullPath());
                return $"{ApplicationDirectory}/Download".GetFullPath();
            }
        }
    }
}
