using SQLite;

namespace Pokemon_3D_Server_Launcher_Game_Module.SQLite.Tables
{
    public class OperatorList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int PlayerID { get; set; }

        public string Reason { get; set; }

        [NotNull]
        public int Permission { get; set; }
    }
}
