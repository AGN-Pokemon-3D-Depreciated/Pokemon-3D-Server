using Modules.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static Pokemon_3D_Server_Launcher_Game_Module.Server.Package.Package;
using static Pokemon_3D_Server_Launcher_Game_Module.Settings.World.World;

namespace Pokemon_3D_Server_Launcher_Game_Module.Server.Player
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

                do
                {
                    if (sw.Elapsed.TotalSeconds >= 1)
                    {
                        sw.Restart();

                        if (CanUpdateWorld)
                        {
                            CanUpdateWorld = false;

                            if (Core.Settings.Features.PlayerInfo)
                            {
                                if (PlayerInfo.Season == SeasonType.Nothing)
                                    Season = Core.World.Season;
                                else
                                    Season = Core.World.GenerateSeason((int)PlayerInfo.Season);

                                if (PlayerInfo.Weather == WeatherType.Nothing)
                                    Weather = Core.World.Weather;
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

                        Network.SentToPlayer(new Package.Package(Core, PackageTypes.WorldData, GenerateWorld(), Network));
                    }

                    if (sw.ElapsedMilliseconds < 1000)
                        Thread.Sleep(1000 - sw.ElapsedMilliseconds.ToInt());
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
