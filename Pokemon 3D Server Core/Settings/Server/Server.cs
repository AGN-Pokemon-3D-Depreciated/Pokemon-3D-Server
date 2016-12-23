using Modules.System;
using Modules.System.Net;
using System.Net;
using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Core.Settings.Server
{
    public class Server
    {
        /// <summary>
        /// Get Protocol Version.
        /// </summary>
        [YamlIgnore]
        public string ProtocolVersion { get; } = "0.5";

        /// <summary>
        /// Get/Set Generate Public IP.
        /// </summary>
        public bool GeneratePublicIP { get; set; } = true;

        /// <summary>
        /// Get/Set IP Address.
        /// </summary>
        [YamlIgnore]
        public IPAddress _IPAddress;
        /// <summary>
        /// Get/Set IP Address.
        /// </summary>
        public string IPAddress
        {
            get { return _IPAddress == null ? "" : _IPAddress.ToString(); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    string TempIPAddress = IPAddressHelper.GetPublicIP();
                    _IPAddress = GeneratePublicIP ? TempIPAddress == null ? null : System.Net.IPAddress.Parse(TempIPAddress) : null;
                }
                else
                    _IPAddress = System.Net.IPAddress.Parse(value);
            }
        }

        /// <summary>
        /// Get/Set Generate Port.
        /// </summary>
        public bool GeneratePort { get; set; } = true;

        private int _Port = 15124;
        /// <summary>
        /// Get/Set Port.
        /// </summary>
        public int Port
        {
            get { return _Port; }
            set { _Port = value.Clamp(0, 65535); }
        }

        /// <summary>
        /// Get/Set Check Port.
        /// </summary>
        public bool CheckPort { get; set; } = true;

        /// <summary>
        /// Get/Set Server Name.
        /// </summary>
        public string ServerName { get; set; } = "P3D Server";

        /// <summary>
        /// Get/Set Server Message.
        /// </summary>
        public string ServerMessage { get; set; } = "A customized P3D Server.";

        /// <summary>
        /// Get/Set Welcome Message.
        /// </summary>
        public string WelcomeMessage { get; set; } = "Welcome to P3D Server. If you need help configuring the server, ask jianmingyong for more details.";

        /// <summary>
        /// Get GameMode.
        /// </summary>
        public GameModes GameMode { get; private set; } = new GameModes();

        private int _MaxPlayers = 20;
        /// <summary>
        /// Get/Set Max Players
        /// </summary>
        public int MaxPlayers
        {
            get { return _MaxPlayers; }
            set
            {
                if (value <= 0)
                    _MaxPlayers = int.MaxValue;
                else
                    _MaxPlayers = value.Clamp(1, int.MaxValue);
            }
        }

        /// <summary>
        /// Get/Set Offline Mode.
        /// </summary>
        public bool OfflineMode { get; set; } = false;

        /// <summary>
        /// Get World.
        /// </summary>
        public World.World World { get; private set; } = new World.World();

        /// <summary>
        /// Get Network.
        /// </summary>
        public Network Network { get; private set; } = new Network();

        /// <summary>
        /// Get Features.
        /// </summary>
        public Features.Features Features { get; set; } = new Features.Features();

        public Server()
        {
            IPAddress = IPAddressHelper.GetPublicIP();
        }
    }
}
