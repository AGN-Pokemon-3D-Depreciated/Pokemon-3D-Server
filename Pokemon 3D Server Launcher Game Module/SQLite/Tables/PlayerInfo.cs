using SQLite;
using System;
using static Pokemon_3D_Server_Launcher_Game_Module.Settings.World.World;

namespace Pokemon_3D_Server_Launcher_Game_Module.SQLite.Tables
{
    public class PlayerInfo
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string GameJoltID { get; set; }

        [NotNull]
        public string IPAddress { get; set; }

        [NotNull]
        public DateTime LastActivity { get; set; } = DateTime.Now;

        [NotNull]
        public SeasonType Season { get; set; } = SeasonType.Nothing;

        [NotNull]
        public WeatherType Weather { get; set; } = WeatherType.Nothing;

        [NotNull]
        public bool DefaultTimeOffset { get; set; } = true;

        [NotNull]
        public int TimeOffset { get; set; } = 0;

        [NotNull]
        public bool DefaultDoDayCycle { get; set; } = true;

        [NotNull]
        public bool DoDayCycle { get; set; } = true;
    }
}
