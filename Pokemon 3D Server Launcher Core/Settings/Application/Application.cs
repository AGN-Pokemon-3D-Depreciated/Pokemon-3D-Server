using System;
using System.Reflection;

namespace Pokemon_3D_Server_Launcher_Core.Settings.Application
{
    internal class Application
    {
        public Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;
    }
}