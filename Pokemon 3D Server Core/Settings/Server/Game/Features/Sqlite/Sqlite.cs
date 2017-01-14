using YamlDotNet.Serialization;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Features.SQLite
{
    public class SQLite
    {
        [YamlMember(Alias = "Database Name")]
        public string DatabaseName { get; set; } = "Pokemon_3D_Data";
    }
}
