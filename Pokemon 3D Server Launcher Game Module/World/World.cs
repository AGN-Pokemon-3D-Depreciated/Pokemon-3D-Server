using Modules.System;
using Pokemon_3D_Server_Launcher_Core.Modules.System.Threading;
using Pokemon_3D_Server_Launcher_Game_Module.Settings.World.SeasonMonth;
using Pokemon_3D_Server_Launcher_Game_Module.Settings.World.WeatherSeason;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Pokemon_3D_Server_Launcher_Game_Module.Settings.World.World;

namespace Pokemon_3D_Server_Launcher_Game_Module.World
{
    public class World : IDisposable
    {
        private Core Core;

        private int _Season;

        public int Season
        {
            get { return _Season; }
            set { _Season = value.RollOver(0, 3); }
        }

        private int _Weather;

        public int Weather
        {
            get { return _Weather; }
            set { _Weather = value.RollOver(0, 9); }
        }

        public int TimeOffset { get; set; }
        public bool DoDayCycle { get; set; }

        private int WeekOfYear { get { return ((DateTime.Now.DayOfYear - (DateTime.Now.DayOfWeek - DayOfWeek.Monday)) / 7.0 + 1.0).Floor().ToInt(); } }

        private bool CanUpdate = true;
        private bool IsActive = false;
        private ThreadHelper Thread = new ThreadHelper();

        public World(Core core)
        {
            Core = core;
            Core.Logger.Log("Global World Initialized.");
        }

        public void Start()
        {
            Thread.Add(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                do
                {
                    try
                    {
                        if (sw.Elapsed.TotalHours >= 1 || !IsActive)
                        {
                            sw.Restart();
                            IsActive = true;

                            if (CanUpdate)
                            {
                                Season = GenerateSeason(Core.Settings.World.Season);
                                Weather = GenerateWeather(Season, Core.Settings.World.Weather);
                                TimeOffset = Core.Settings.World.TimeOffset;
                                DoDayCycle = Core.Settings.World.DoDayCycle;
                            }

                            Core.Logger.Log(Core.World.ToString());
                            Core.TcpClientCollection.UpdateWorld();
                        }

                        if (sw.Elapsed.TotalHours < 1)
                            Thread.Sleep(1000);
                    }
                    catch (Exception) { }
                } while (IsActive);
            });
        }

        public void Stop()
        {
            IsActive = false;
        }

        public int GenerateSeason(int season)
        {
            switch (season)
            {
                case (int)SeasonType.DefaultSeason:
                    switch (Core.World.WeekOfYear % 4)
                    {
                        case 0:
                            return (int)SeasonType.Fall;

                        case 1:
                            return (int)SeasonType.Winter;

                        case 2:
                            return (int)SeasonType.Spring;

                        case 3:
                            return (int)SeasonType.Summer;

                        default:
                            return (int)SeasonType.Summer;
                    }

                case (int)SeasonType.Random:
                    return MathHelper.Random(0, 3);

                case (int)SeasonType.Custom:
                    return GetCustomSeason();

                default:
                    return Core.Settings.World.Season;
            }
        }

        public int GenerateWeather(int season, int weather)
        {
            switch (weather)
            {
                case (int)WeatherType.DefaultWeather:
                    int Random = MathHelper.Random(1, 100);

                    switch (season)
                    {
                        case (int)SeasonType.Winter:
                            if (Random > 50)
                                return (int)WeatherType.Snow;
                            else if (Random > 20)
                                return (int)WeatherType.Clear;
                            else
                                return (int)WeatherType.Rain;

                        case (int)SeasonType.Spring:
                            if (Random > 40)
                                return (int)WeatherType.Clear;
                            else if (Random > 5)
                                return (int)WeatherType.Rain;
                            else
                                return (int)WeatherType.Snow;

                        case (int)SeasonType.Summer:
                            if (Random > 10)
                                return (int)WeatherType.Clear;
                            else
                                return (int)WeatherType.Rain;

                        case (int)SeasonType.Fall:
                            if (Random > 80)
                                return (int)WeatherType.Clear;
                            else if (Random > 5)
                                return (int)WeatherType.Rain;
                            else
                                return (int)WeatherType.Snow;

                        default:
                            return (int)WeatherType.Clear;
                    }

                case (int)WeatherType.Random:
                    return MathHelper.Random(0, 9);

                case (int)WeatherType.Custom:
                    return GetCustomWeather();

                default:
                    return Core.Settings.World.Weather;
            }
        }

        private int GetCustomSeason()
        {
            try
            {
                List<Season> seasonMonthRef;
                double totalChance = 0;

                switch (DateTime.Now.Month)
                {
                    case 1:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.January;
                        break;

                    case 2:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.February;
                        break;

                    case 3:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.March;
                        break;

                    case 4:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.April;
                        break;

                    case 5:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.May;
                        break;

                    case 6:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.June;
                        break;

                    case 7:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.July;
                        break;

                    case 8:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.August;
                        break;

                    case 9:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.September;
                        break;

                    case 10:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.October;
                        break;

                    case 11:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.November;
                        break;

                    case 12:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.December;
                        break;

                    default:
                        seasonMonthRef = Core.Settings.World.SeasonMonth.January;
                        break;
                }

                foreach (Season Season in seasonMonthRef)
                    totalChance += Season.Chance;

                double currentChance = 0;
                int randomChance = MathHelper.Random(1, totalChance.Floor().ToString().ToInt());

                foreach (Season season in seasonMonthRef)
                {
                    currentChance += season.Chance;

                    if (randomChance <= currentChance)
                        return season.Value.Clamp(0, 3);
                }

                return (int)SeasonType.Winter;
            }
            catch (Exception)
            {
                return (int)SeasonType.Winter;
            }
        }

        private int GetCustomWeather()
        {
            try
            {
                List<Weather> weatherSeasonRef;
                double totalChance = 0;

                switch (Season)
                {
                    case (int)SeasonType.Winter:
                        weatherSeasonRef = Core.Settings.World.WeatherSeason.Winter;
                        break;

                    case (int)SeasonType.Spring:
                        weatherSeasonRef = Core.Settings.World.WeatherSeason.Spring;
                        break;

                    case (int)SeasonType.Summer:
                        weatherSeasonRef = Core.Settings.World.WeatherSeason.Summer;
                        break;

                    case (int)SeasonType.Fall:
                        weatherSeasonRef = Core.Settings.World.WeatherSeason.Fall;
                        break;

                    default:
                        weatherSeasonRef = Core.Settings.World.WeatherSeason.Winter;
                        break;
                }

                foreach (Weather weather in weatherSeasonRef)
                {
                    totalChance += weather.Chance;
                }

                double currentChance = 0;
                int randomChance = MathHelper.Random(1, totalChance.Floor().ToString().ToInt());

                foreach (Weather weather in weatherSeasonRef)
                {
                    currentChance += weather.Chance;

                    if (randomChance <= currentChance)
                        return weather.Value.Clamp(0, 9);
                }

                return (int)WeatherType.Clear;
            }
            catch (Exception)
            {
                return (int)WeatherType.Clear;
            }
        }

        public override string ToString()
        {
            return $"Current Season: {Core.Settings.World.GetSeasonName(Season)} | Current Weather: {Core.Settings.World.GetWeatherName(Weather)} | Current Time: {DateTime.Now.AddSeconds(TimeOffset).ToString("dd/MM/yyyy hh:mm:ss tt")}";
        }

        public void Dispose()
        {
            Thread.Dispose();
            Core.Logger.Log("Global World Disposed.");
        }
    }
}