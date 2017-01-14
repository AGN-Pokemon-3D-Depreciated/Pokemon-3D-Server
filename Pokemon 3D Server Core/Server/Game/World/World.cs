using Modules.System;
using Modules.System.Threading;
using Pokemon_3D_Server_Core.Interface;
using Pokemon_3D_Server_Core.Settings.Server.Game.World.SeasonMonth;
using Pokemon_3D_Server_Core.Settings.Server.Game.World.WeatherSeason;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Pokemon_3D_Server_Core.Collections.SeasonTypeCollection;
using static Pokemon_3D_Server_Core.Collections.WeatherTypeCollection;

namespace Pokemon_3D_Server_Core.Server.Game.World
{
    public class World : IModules
    {
        public string Name { get; } = "Game World";
        public string Version { get; } = "0.54";

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

        public int TimeOffset { get; set; } = 0;
        public bool DoDayCycle { get; set; }

        private int WeekOfYear { get { return ((DateTime.Now.DayOfYear - (DateTime.Now.DayOfWeek - DayOfWeek.Monday)) / 7.0 + 1.0).Floor().ToInt(); } }
        private bool CanUpdate = true;
        private bool IsActive = false;
        private ThreadHelper Thread = new ThreadHelper();

        public void Start()
        {
            Thread.Add(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Stopwatch sw2 = new Stopwatch();
                sw2.Start();

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
                                Season = GenerateSeason(Core.Settings.Server.Game.World.Season);
                                Weather = GenerateWeather(Season, Core.Settings.Server.Game.World.Weather);
                                TimeOffset = Core.Settings.Server.Game.World.TimeOffset;
                                DoDayCycle = Core.Settings.Server.Game.World.DoDayCycle;
                            }

                            Core.Logger.Log(Core.World.ToString());
                            Core.TcpClientCollection.UpdateWorld();
                        }
                        else
                        {
                            sw2.Stop();
                            if (sw2.ElapsedMilliseconds < 1000)
                                Thread.Sleep(1000 - sw2.ElapsedMilliseconds.ToInt());
                            sw2.Restart();
                        }
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
                    return Core.Settings.Server.Game.World.Season;
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
                    return Core.Settings.Server.Game.World.Weather;
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
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.January;
                        break;

                    case 2:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.February;
                        break;

                    case 3:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.March;
                        break;

                    case 4:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.April;
                        break;

                    case 5:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.May;
                        break;

                    case 6:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.June;
                        break;

                    case 7:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.July;
                        break;

                    case 8:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.August;
                        break;

                    case 9:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.September;
                        break;

                    case 10:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.October;
                        break;

                    case 11:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.November;
                        break;

                    case 12:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.December;
                        break;

                    default:
                        seasonMonthRef = Core.Settings.Server.Game.World.SeasonMonth.January;
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
                        weatherSeasonRef = Core.Settings.Server.Game.World.WeatherSeason.Winter;
                        break;

                    case (int)SeasonType.Spring:
                        weatherSeasonRef = Core.Settings.Server.Game.World.WeatherSeason.Spring;
                        break;

                    case (int)SeasonType.Summer:
                        weatherSeasonRef = Core.Settings.Server.Game.World.WeatherSeason.Summer;
                        break;

                    case (int)SeasonType.Fall:
                        weatherSeasonRef = Core.Settings.Server.Game.World.WeatherSeason.Fall;
                        break;

                    default:
                        weatherSeasonRef = Core.Settings.Server.Game.World.WeatherSeason.Winter;
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
            return $"Current Season: {Core.Settings.Server.Game.World.GetSeasonName(Season)} | Current Weather: {Core.Settings.Server.Game.World.GetWeatherName(Weather)} | Current Time: {DateTime.Now.AddSeconds(TimeOffset).ToString("dd/MM/yyyy hh:mm:ss tt")}";
        }
    }
}
