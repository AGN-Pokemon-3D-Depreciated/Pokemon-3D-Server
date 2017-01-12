using SQLite;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Features
{
    public class WhiteList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int PlayerID { get; set; }

        public string Reason { get; set; }
    }
}
