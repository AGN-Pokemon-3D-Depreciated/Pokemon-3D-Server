using Modules.System;
using Modules.System.Net;
using System.Net;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Network
{
    public class Network
    {
        [YamlIgnore]
        public string ProtocolVersion { get; } = "0.5";

        public bool GeneratePublicIP { get; set; } = true;

        private IPAddress _IPAddress;
        public string IPAddress
        {
            get { return _IPAddress == null ? "" : _IPAddress.ToString(); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string tempIPAddress = IPAddressHelper.GetPublicIP();
                    _IPAddress = GeneratePublicIP ? tempIPAddress == null ? null : System.Net.IPAddress.Parse(tempIPAddress) : null;
                }
                else
                    _IPAddress = System.Net.IPAddress.Parse(value);
            }
        }

        private int _Port = 15124;
        public int Port
        {
            get { return _Port; }
            set { _Port = value.Clamp(0, 65535); }
        }

        public bool CheckPort { get; set; } = true;

        public Network()
        {
            IPAddress = IPAddressHelper.GetPublicIP();
        }
    }
}
