using SQLite;
using System;
using static Pokemon_3D_Server_Core.Collections.SeasonTypeCollection;
using static Pokemon_3D_Server_Core.Collections.WeatherTypeCollection;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.Features
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
        public DateTime LastActivity { get; set; }

        [NotNull]
        public SeasonType Season { get; set; } = SeasonType.Nothing;

        [NotNull]
        public WeatherType Weather { get; set; } = WeatherType.Nothing;

        [NotNull]
        public int TimeOffset { get; set; } = 0;

        [NotNull]
        public bool DoDayCycle { get; set; }
    }
}
