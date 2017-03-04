using Modules.System;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Launcher_Game_Module.Settings.Network
{
    public class Network
    {
        [YamlIgnore]
        public string ProtocolVersion { get; } = "0.5";

        private int _Port = 15124;

        public int Port
        {
            get { return _Port; }
            set { _Port = value.Clamp(1, 65535); }
        }

        public bool CheckPort { get; set; } = true;
    }
}
