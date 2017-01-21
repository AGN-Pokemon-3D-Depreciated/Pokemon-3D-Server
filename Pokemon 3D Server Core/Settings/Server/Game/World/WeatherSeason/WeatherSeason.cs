using System.Collections.Generic;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.World.WeatherSeason
{
    public class WeatherSeason
    {
        public List<Weather> Winter { get; private set; } = new List<Weather> { new Weather() };
        public List<Weather> Spring { get; private set; } = new List<Weather> { new Weather() };
        public List<Weather> Summer { get; private set; } = new List<Weather> { new Weather() };
        public List<Weather> Fall { get; private set; } = new List<Weather> { new Weather() };
    }
}