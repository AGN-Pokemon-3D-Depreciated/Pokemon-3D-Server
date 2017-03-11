using static Pokemon_3D_Server_Launcher_Game_Module.Settings.World.World;

namespace Pokemon_3D_Server_Launcher_Game_Module.Settings.World.WeatherSeason
{
    public class Weather
    {
        public int Value { get; set; } = (int)WeatherType.DefaultWeather;
        public double Chance { get; set; } = 100.0;
    }
}
