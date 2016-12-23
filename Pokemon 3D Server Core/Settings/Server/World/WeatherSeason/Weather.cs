using static Pokemon_3D_Server_Core.Settings.Server.World.World;

namespace Pokemon_3D_Server_Core.Settings.Server.World.WeatherSeason
{
    public class Weather
    {
        /// <summary>
        /// Get/Set weather value.
        /// </summary>
        public int Value { get; set; } = (int)WeatherType.DefaultWeather;

        /// <summary>
        /// Get/Set Chance.
        /// </summary>
        public double Chance { get; set; } = 100.0;
    }
}
