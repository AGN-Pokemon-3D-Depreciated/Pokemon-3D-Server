using SQLite;
using static Pokemon_3D_Server_Core.Collections.OperatorTypeCollection;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Features
{
    public class OperatorList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public int PlayerID { get; set; }

        public string Reason { get; set; }

        [NotNull]
        public OperatorTypes Permission { get; set; }
    }
}
