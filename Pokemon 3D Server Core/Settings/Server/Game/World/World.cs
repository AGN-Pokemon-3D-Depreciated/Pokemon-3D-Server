using Modules.System;
using static Pokemon_3D_Server_Core.Collections.SeasonTypeCollection;
using static Pokemon_3D_Server_Core.Collections.WeatherTypeCollection;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.World
{
    public class World
    {
        private int _Season = (int)SeasonType.DefaultSeason;

        public int Season
        {
            get { return _Season; }
            set { _Season = value.Clamp(-3, 3); }
        }

        private int _Weather = (int)WeatherType.DefaultWeather;

        public int Weather
        {
            get { return _Weather; }
            set { _Weather = value.Clamp(-4, 9); }
        }

        public bool DoDayCycle { get; set; } = true;
        public int TimeOffset { get; set; } = 0;

        public SeasonMonth.SeasonMonth SeasonMonth { get; private set; } = new SeasonMonth.SeasonMonth();
        public WeatherSeason.WeatherSeason WeatherSeason { get; private set; } = new WeatherSeason.WeatherSeason();

        public string GetSeasonName(int season)
        {
            switch (season)
            {
                case (int)SeasonType.Winter:
                    return "Winter";

                case (int)SeasonType.Spring:
                    return "Spring";

                case (int)SeasonType.Summer:
                    return "Summer";

                case (int)SeasonType.Fall:
                    return "Fall";

                default:
                    return "Winter";
            }
        }

        public string GetWeatherName(int weather)
        {
            switch (weather)
            {
                case (int)WeatherType.Ash:
                    return "Ash";

                case (int)WeatherType.Blizzard:
                    return "Blizzard";

                case (int)WeatherType.Clear:
                    return "Clear";

                case (int)WeatherType.Fog:
                    return "Fog";

                case (int)WeatherType.Rain:
                    return "Rain";

                case (int)WeatherType.Sandstorm:
                    return "Sandstorm";

                case (int)WeatherType.Snow:
                    return "Snow";

                case (int)WeatherType.Sunny:
                    return "Sunny";

                case (int)WeatherType.Thunderstorm:
                    return "Thunderstorm";

                case (int)WeatherType.Underwater:
                    return "Underwater";

                default:
                    return "Clear";
            }
        }
    }
}