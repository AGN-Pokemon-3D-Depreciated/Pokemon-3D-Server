using SQLite;
using System;
using static Pokemon_3D_Server_Core.Settings.Server.World.World;

namespace Pokemon_3D_Server_Core.Server.Game.SQLite.Models
{
    public class PlayerInfo
    {
        /// <summary>
        /// Get/Set Player ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Get/Set Player Name.
        /// </summary>
        [NotNull]
        public string Name { get; set; }

        /// <summary>
        /// Get/Set Player GameJolt ID.
        /// </summary>
        [NotNull]
        public string GameJoltID { get; set; }

        /// <summary>
        /// Get/Set Player IP Address.
        /// </summary>
        [NotNull]
        public string IPAddress { get; set; }

        /// <summary>
        /// Get/Set Player Last Activity.
        /// </summary>
        [NotNull]
        public DateTime LastActivity { get; set; }

        /// <summary>
        /// Get/Set Player Season Type.
        /// </summary>
        [NotNull]
        public SeasonType Season { get; set; } = SeasonType.Nothing;

        /// <summary>
        /// Get/Set Player Weather Type.
        /// </summary>
        [NotNull]
        public WeatherType Weather { get; set; } = WeatherType.Nothing;

        /// <summary>
        /// Get/Set Player Time Offset.
        /// </summary>
        [NotNull]
        public int TimeOffset { get; set; } = 0;

        /// <summary>
        /// Get/Set Player Do Day Cycle.
        /// </summary>
        [NotNull]
        public bool DoDayCycle { get; set; }
    }
}
