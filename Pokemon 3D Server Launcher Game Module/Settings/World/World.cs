using Modules.System;

namespace Pokemon_3D_Server_Launcher_Game_Module.Settings.World
{
    public class World
    {
        public enum SeasonType
        {
            Winter = 0,
            Spring = 1,
            Summer = 2,
            Fall = 3,

            Random = -1,
            DefaultSeason = -2,
            Custom = -3,
            Nothing = -4,
        }

        public enum WeatherType
        {
            Clear = 0,
            Rain = 1,
            Snow = 2,
            Underwater = 3,
            Sunny = 4,
            Fog = 5,
            Thunderstorm = 6,
            Sandstorm = 7,
            Ash = 8,
            Blizzard = 9,

            Random = -1,
            DefaultWeather = -2,
            Custom = -3,
            Nothing = -4,
        }

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
