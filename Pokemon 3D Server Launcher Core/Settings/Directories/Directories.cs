using Modules.System.IO;
using System;

namespace Pokemon_3D_Server_Launcher_Core.Settings.Directories
{
    public sealed class Directories
    {
        public string ApplicationDirectory { get; } = AppDomain.CurrentDomain.BaseDirectory.GetFullPath();

        public string CrashLogDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/CrashLogs".GetFullPath());
                return $"{ApplicationDirectory}/CrashLogs".GetFullPath();
            }
        }

        public string DataDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/Data".GetFullPath());
                return $"{ApplicationDirectory}/Data".GetFullPath();
            }
        }

        public string LoggerDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/Logger".GetFullPath());
                return $"{ApplicationDirectory}/Logger".GetFullPath();
            }
        }

        public string DownloadDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/Download".GetFullPath());
                return $"{ApplicationDirectory}/Download".GetFullPath();
            }
        }

        public string ModulesDirectory
        {
            get
            {
                DirectoryHelper.CreateDirectoryIfNotExists($"{ApplicationDirectory}/Modules".GetFullPath());
                return $"{ApplicationDirectory}/Modules".GetFullPath();
            }
        }
    }
}