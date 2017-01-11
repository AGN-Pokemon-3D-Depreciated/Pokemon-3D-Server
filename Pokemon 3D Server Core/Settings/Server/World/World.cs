using Modules.System;

namespace Pokemon_3D_Server_Core.Settings.Server.World
{
    public class World
    {
        /// <summary>
        /// A collection of Season Type
        /// </summary>
        public enum SeasonType
        {
            /// <summary>
            /// Winter Season
            /// </summary>
            Winter = 0,

            /// <summary>
            /// Spring Season
            /// </summary>
            Spring = 1,

            /// <summary>
            /// Summer Season
            /// </summary>
            Summer = 2,

            /// <summary>
            /// Fall Season
            /// </summary>
            Fall = 3,

            /// <summary>
            /// Random Season
            /// </summary>
            Random = -1,

            /// <summary>
            /// Default Server Season
            /// </summary>
            DefaultSeason = -2,

            /// <summary>
            /// Custom Server Season
            /// </summary>
            Custom = -3,

            /// <summary>
            /// Nothing (Only used in command)
            /// </summary>
            Nothing = -4,
        }

        /// <summary>
        /// A collection of Weather Type
        /// </summary>
        public enum WeatherType
        {
            /// <summary>
            /// Clear Weather
            /// </summary>
            Clear = 0,

            /// <summary>
            /// Rain Weather
            /// </summary>
            Rain = 1,

            /// <summary>
            /// Snow Weather
            /// </summary>
            Snow = 2,

            /// <summary>
            /// Underwater Weather
            /// </summary>
            Underwater = 3,

            /// <summary>
            /// Sunny Weather
            /// </summary>
            Sunny = 4,

            /// <summary>
            /// Fog Weather
            /// </summary>
            Fog = 5,

            /// <summary>
            /// Thunderstorm Weather
            /// </summary>
            Thunderstorm = 6,

            /// <summary>
            /// Sandstorm Weather
            /// </summary>
            Sandstorm = 7,

            /// <summary>
            /// Ash Weather
            /// </summary>
            Ash = 8,

            /// <summary>
            /// Blizzard Weather
            /// </summary>
            Blizzard = 9,

            /// <summary>
            /// Random Weather
            /// </summary>
            Random = -1,

            /// <summary>
            /// Default Server Weather
            /// </summary>
            DefaultWeather = -2,

            /// <summary>
            /// Custom Server Weather
            /// </summary>
            Custom = -3,

            /// <summary>
            /// Real World Weather
            /// </summary>
            Real = -4,

            /// <summary>
            /// Nothing (used in server command)
            /// </summary>
            Nothing = -5,
        }

        private int _Season = (int)SeasonType.DefaultSeason;
        /// <summary>
        /// Get/Set Season.
        /// </summary>
        public int Season
        {
            get { return _Season; }
            set { _Season = value.Clamp(-3, 3); }
        }

        private int _Weather = (int)WeatherType.DefaultWeather;
        /// <summary>
        /// Get/Set Weather.
        /// </summary>
        public int Weather
        {
            get { return _Weather; }
            set { _Weather = value.Clamp(-4, 9); }
        }

        /// <summary>
        /// Get/Set Do DayCycle.
        /// </summary>
        public bool DoDayCycle { get; set; } = true;

        /// <summary>
        /// Get/Set Time Offset.
        /// </summary>
        public int TimeOffset { get; set; } = 0;

        /// <summary>
        /// Get Season Month.
        /// </summary>
        public SeasonMonth.SeasonMonth SeasonMonth { get; private set; } = new SeasonMonth.SeasonMonth();

        /// <summary>
        /// Get Weather Season.
        /// </summary>
        public WeatherSeason.WeatherSeason WeatherSeason { get; private set; } = new WeatherSeason.WeatherSeason();

        /// <summary>
        /// Get Season Name
        /// </summary>
        /// <param name="season">Season ID</param>
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

        /// <summary>
        /// Get Weather Name
        /// </summary>
        /// <param name="weather">Weather ID</param>
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
