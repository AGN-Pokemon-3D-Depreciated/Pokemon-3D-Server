using System.Collections.Generic;

namespace Pokemon_3D_Server_Launcher_Game_Module.Settings.World.WeatherSeason
{
    public class WeatherSeason
    {
        public List<Weather> Winter { get; private set; } = new List<Weather> { new Weather() };
        public List<Weather> Spring { get; private set; } = new List<Weather> { new Weather() };
        public List<Weather> Summer { get; private set; } = new List<Weather> { new Weather() };
        public List<Weather> Fall { get; private set; } = new List<Weather> { new Weather() };
    }
}
