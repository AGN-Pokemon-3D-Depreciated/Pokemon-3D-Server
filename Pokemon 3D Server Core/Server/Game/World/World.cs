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

        private DateTime LastWorldUpdate;
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
                        if ((!IsActive || sw.Elapsed.TotalHours >= 1) && CanUpdate)
                        {
                            sw.Restart();

                            switch (Core.Settings.Server.Game.World.Season)
                            {
                                case (int)SeasonType.DefaultSeason:
                                    switch (WeekOfYear % 4)
                                    {
                                        case 0:
                                            Season = (int)SeasonType.Fall;
                                            break;

                                        case 1:
                                            Season = (int)SeasonType.Winter;
                                            break;

                                        case 2:
                                            Season = (int)SeasonType.Spring;
                                            break;

                                        case 3:
                                            Season = (int)SeasonType.Summer;
                                            break;

                                        default:
                                            Season = (int)SeasonType.Summer;
                                            break;
                                    }
                                    break;

                                case (int)SeasonType.Random:
                                    Season = MathHelper.Random(0, 3);
                                    break;

                                case (int)SeasonType.Custom:
                                    Season = GetCustomSeason();
                                    break;

                                default:
                                    Season = Core.Settings.Server.Game.World.Season;
                                    break;
                            }

                            switch (Core.Settings.Server.Game.World.Weather)
                            {
                                case (int)WeatherType.DefaultWeather:
                                    int Random = MathHelper.Random(1, 100);

                                    switch (Season)
                                    {
                                        case (int)SeasonType.Winter:
                                            if (Random > 50)
                                                Weather = (int)WeatherType.Snow;
                                            else if (Random > 20)
                                                Weather = (int)WeatherType.Clear;
                                            else
                                                Weather = (int)WeatherType.Rain;
                                            break;

                                        case (int)SeasonType.Spring:
                                            if (Random > 40)
                                                Weather = (int)WeatherType.Clear;
                                            else if (Random > 5)
                                                Weather = (int)WeatherType.Rain;
                                            else
                                                Weather = (int)WeatherType.Snow;
                                            break;

                                        case (int)SeasonType.Summer:
                                            if (Random > 10)
                                                Weather = (int)WeatherType.Clear;
                                            else
                                                Weather = (int)WeatherType.Rain;
                                            break;

                                        case (int)SeasonType.Fall:
                                            if (Random > 80)
                                                Weather = (int)WeatherType.Clear;
                                            else if (Random > 5)
                                                Weather = (int)WeatherType.Rain;
                                            else
                                                Weather = (int)WeatherType.Snow;
                                            break;

                                        default:
                                            Weather = (int)WeatherType.Clear;
                                            break;
                                    }
                                    break;

                                case (int)WeatherType.Random:
                                    Weather = MathHelper.Random(0, 9);
                                    break;

                                case (int)WeatherType.Custom:
                                    Weather = GetCustomWeather();
                                    break;

                                default:
                                    Weather = Core.Settings.Server.Game.World.Weather;
                                    break;
                            }

                            IsActive = true;
                            TimeOffset = Core.Settings.Server.Game.World.TimeOffset;
                            LastWorldUpdate = DateTime.Now;

                            Core.Logger.Log(ToString());
                        }
                        else
                        {
                            sw2.Stop();
                            if (sw2.ElapsedMilliseconds < 1000)
                                Thread.Sleep(1000 - sw2.ElapsedMilliseconds.ToString().ToInt());
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

        public List<string> GenerateWorld()
        {
            return new List<string>
            {
                Season.ToString(),
                Weather.ToString(),
                Core.Settings.Server.Game.World.DoDayCycle ? DateTime.Now.AddSeconds(TimeOffset).ToString("H,m,s") : "12,0,0"
            };
        }

        public override string ToString()
        {
            return $"Current Season: {Core.Settings.Server.Game.World.GetSeasonName(Season)} | Current Weather: {Core.Settings.Server.Game.World.GetWeatherName(Weather)} | Current Time: {DateTime.Now.AddSeconds(TimeOffset).ToString("dd/MM/yyyy hh:mm:ss tt")}";
        }
    }
}
