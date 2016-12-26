using Modules.System;
using Modules.System.Threading;
using Pokemon_3D_Server_Core.Interface;
using Pokemon_3D_Server_Core.Settings.Server.World.SeasonMonth;
using Pokemon_3D_Server_Core.Settings.Server.World.WeatherSeason;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Pokemon_3D_Server_Core.Settings.Server.World.World;

namespace Pokemon_3D_Server_Core.Server.Game.World
{
    public class World : IModules
    {
        /// <summary>
        /// Get the name of the module.
        /// </summary>
        public string Name { get { return "Game World"; } }

        /// <summary>
        /// Get the version of the module.
        /// </summary>
        public string Version { get { return "0.54"; } }

        private int _Season;
        /// <summary>
        /// Get/Set Current World Season
        /// </summary>
        public int Season
        {
            get
            {
                return _Season;
            }
            set
            {
                _Season = value.RollOver(0, 3);

            }
        }

        private int _Weather;
        /// <summary>
        /// Get/Set Current World Weather
        /// </summary>
        public int Weather
        {
            get
            {
                return _Weather;
            }
            set
            {
                _Weather = value.RollOver(0, 9);
            }
        }

        /// <summary>
        /// Get/Set Current World Time Offset
        /// </summary>
        public int TimeOffset { get; internal set; } = 0;

        private DateTime LastWorldUpdate;
        private int WeekOfYear { get { return ((DateTime.Now.DayOfYear - (DateTime.Now.DayOfWeek - DayOfWeek.Monday)) / 7.0 + 1.0).Floor().ToString().ToInt(); } }

        private bool CanUpdate = true;
        private bool IsActive = false;

        private ThreadHelper Thread = new ThreadHelper();

        /// <summary>
        /// Start the module.
        /// </summary>
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

                            // Get Setting Season
                            switch (Core.Settings.Server.World.Season)
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
                                    Season = Core.Settings.Server.World.Season;
                                    break;
                            }

                            // Get Setting Weather
                            switch (Core.Settings.Server.World.Weather)
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
                                    Weather = Core.Settings.Server.World.Weather;
                                    break;
                            }

                            IsActive = true;
                            TimeOffset = Core.Settings.Server.World.TimeOffset;
                            LastWorldUpdate = DateTime.Now;

                            Core.Logger.Log($"Current Season: {Core.Settings.Server.World.GetSeasonName(Season)} | Current Weather: {Core.Settings.Server.World.GetWeatherName(Weather)} | Current Time: {DateTime.Now.AddSeconds(TimeOffset).ToString("dd/MM/yyyy hh:mm:ss tt")}");
                        }
                        else
                        {
                            sw2.Stop();
                            if (sw2.ElapsedMilliseconds < 1000)
                            {
                                Thread.Sleep(1000 - sw2.ElapsedMilliseconds.ToString().ToInt());
                            }
                            sw2.Restart();
                        }
                    }
                    catch (Exception) { }
                } while (IsActive);
            });
        }

        /// <summary>
        /// Stop the module.
        /// </summary>
        public void Stop()
        {
            IsActive = false;
        }

        private int GetCustomSeason()
        {
            try
            {
                List<Season> SeasonMonthRef;
                double TotalChance = 0;

                switch (DateTime.Now.Month)
                {
                    case 1:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.January;
                        break;

                    case 2:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.February;
                        break;

                    case 3:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.March;
                        break;

                    case 4:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.April;
                        break;

                    case 5:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.May;
                        break;

                    case 6:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.June;
                        break;

                    case 7:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.July;
                        break;

                    case 8:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.August;
                        break;

                    case 9:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.September;
                        break;

                    case 10:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.October;
                        break;

                    case 11:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.November;
                        break;

                    case 12:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.December;
                        break;

                    default:
                        SeasonMonthRef = Core.Settings.Server.World.SeasonMonth.January;
                        break;
                }

                foreach (Season Season in SeasonMonthRef)
                    TotalChance += Season.Chance;

                double CurrentChance = 0;
                int RandomChance = MathHelper.Random(1, TotalChance.Floor().ToString().ToInt());

                foreach (Season Season in SeasonMonthRef)
                {
                    CurrentChance += Season.Chance;

                    if (RandomChance <= CurrentChance)
                        return Season.Value.Clamp(0, 3);
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
                List<Weather> WeatherSeasonRef;
                double TotalChance = 0;

                switch (Season)
                {
                    case (int)SeasonType.Winter:
                        WeatherSeasonRef = Core.Settings.Server.World.WeatherSeason.Winter;
                        break;

                    case (int)SeasonType.Spring:
                        WeatherSeasonRef = Core.Settings.Server.World.WeatherSeason.Spring;
                        break;

                    case (int)SeasonType.Summer:
                        WeatherSeasonRef = Core.Settings.Server.World.WeatherSeason.Summer;
                        break;

                    case (int)SeasonType.Fall:
                        WeatherSeasonRef = Core.Settings.Server.World.WeatherSeason.Fall;
                        break;

                    default:
                        WeatherSeasonRef = Core.Settings.Server.World.WeatherSeason.Winter;
                        break;
                }

                foreach (Weather Weather in WeatherSeasonRef)
                {
                    TotalChance += Weather.Chance;
                }

                double CurrentChance = 0;
                int RandomChance = MathHelper.Random(1, TotalChance.Floor().ToString().ToInt());

                foreach (Weather Weather in WeatherSeasonRef)
                {
                    CurrentChance += Weather.Chance;

                    if (RandomChance <= CurrentChance)
                        return Weather.Value.Clamp(0, 9);
                }

                return (int)WeatherType.Clear;
            }
            catch (Exception)
            {
                return (int)WeatherType.Clear;
            }
        }

        /// <summary>
        /// Generate Global World Data
        /// </summary>
        public List<string> GenerateWorld()
        {
            return new List<string>
            {
                Season.ToString(),
                Weather.ToString(),
                Core.Settings.Server.World.DoDayCycle ? DateTime.Now.AddSeconds(TimeOffset).ToString("H,m,s") : "12,0,0"
            };
        }

        /// <summary>
        /// Get current World
        /// </summary>
        public override string ToString()
        {
            return string.Format(@"Current Season: {0} | Current Weather: {1} | Current Time: {2}", Core.Settings.Server.World.GetSeasonName(Season), Core.Settings.Server.World.GetWeatherName(Weather), DateTime.Now.AddSeconds(TimeOffset).ToString());
        }
    }
}
