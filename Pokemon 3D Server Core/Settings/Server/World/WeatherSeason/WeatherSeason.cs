using System.Collections.Generic;

namespace Pokemon_3D_Server_Core.Settings.Server.World.WeatherSeason
{
    public class WeatherSeason
    {
        /// <summary>
        /// Get Winter Season.
        /// </summary>
        public List<Weather> Winter { get; private set; } = new List<Weather> { new Weather() };

        /// <summary>
        /// Get Spring Season.
        /// </summary>
        public List<Weather> Spring { get; private set; } = new List<Weather> { new Weather() };

        /// <summary>
        /// Get Summer Season.
        /// </summary>
        public List<Weather> Summer { get; private set; } = new List<Weather> { new Weather() };

        /// <summary>
        /// Get Summer Season.
        /// </summary>
        public List<Weather> Fall { get; private set; } = new List<Weather> { new Weather() };
    }
}
