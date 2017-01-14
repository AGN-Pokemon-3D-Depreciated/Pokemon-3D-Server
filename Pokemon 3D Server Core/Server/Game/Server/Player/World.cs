using Modules.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Pokemon_3D_Server_Core.Collections.SeasonTypeCollection;
using static Pokemon_3D_Server_Core.Collections.WeatherTypeCollection;
using static Pokemon_3D_Server_Core.Server.Game.Server.Package.Package;

namespace Pokemon_3D_Server_Core.Server.Game.Server.Player
{
    public partial class Player
    {
        public bool CanUpdateWorld { get; set; } = true;

        private int Season;
        private int Weather;
        private int TimeOffset;
        private bool DoDayCycle;

        private void UpdateWorld()
        {
            Thread.Add(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                Stopwatch sw2 = new Stopwatch();
                sw2.Start();

                do
                {
                    if (sw.Elapsed.TotalSeconds >= 1)
                    {
                        sw.Restart();

                        if (CanUpdateWorld)
                        {
                            CanUpdateWorld = false;

                            if (Core.Settings.Server.Game.Features.PlayerInfo)
                            {
                                if (PlayerInfo.Season == SeasonType.Nothing)
                                    Season = Core.World.Season;
                                else
                                    Season = Core.World.GenerateSeason((int)PlayerInfo.Season);

                                if (PlayerInfo.Weather == WeatherType.Nothing)
                                    Weather = Core.World.Season;
                                else
                                    Weather = Core.World.GenerateWeather(Season, (int)PlayerInfo.Weather);

                                if (PlayerInfo.DefaultTimeOffset)
                                    TimeOffset = Core.World.TimeOffset;
                                else
                                    TimeOffset = PlayerInfo.TimeOffset;

                                if (PlayerInfo.DefaultDoDayCycle)
                                    DoDayCycle = Core.World.DoDayCycle;
                                else
                                    DoDayCycle = PlayerInfo.DoDayCycle;
                            }
                        }

                        Network.SentToPlayer(new Package.Package(PackageTypes.WorldData, GenerateWorld(), Network));
                    }
                    else
                    {
                        sw2.Stop();
                        if (sw2.ElapsedMilliseconds < 1000)
                            Thread.Sleep(1000 - sw2.ElapsedMilliseconds.ToInt());
                        sw2.Restart();
                    }
                } while (Network.IsActive);
            });
        }

        private List<string> GenerateWorld()
        {
            return new List<string>
            {
                Season.ToString(),
                Weather.ToString(),
                DoDayCycle ? DateTime.Now.AddSeconds(TimeOffset).ToString("H,m,s") : "12,0,0"
            };
        }
    }
}
