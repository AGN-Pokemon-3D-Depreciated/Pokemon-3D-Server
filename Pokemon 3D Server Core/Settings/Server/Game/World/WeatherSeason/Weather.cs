using static Pokemon_3D_Server_Core.Collections.WeatherTypeCollection;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.World.WeatherSeason
{
    public class Weather
    {
        public int Value { get; set; } = (int)WeatherType.DefaultWeather;
        public double Chance { get; set; } = 100.0;
    }
}