using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Core.Settings.Server.Features
{
    public class Sqlite
    {
        /// <summary>
        /// Get/Set Name.
        /// </summary>
        [YamlMember(Alias = "Database Name")]
        public string Name { get; set; } = "Pokemon_3D_Data";
    }
}
