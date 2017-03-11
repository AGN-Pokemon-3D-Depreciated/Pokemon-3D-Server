using static Pokemon_3D_Server_Launcher_Game_Module.Settings.World.World;

namespace Pokemon_3D_Server_Launcher_Game_Module.Settings.World.SeasonMonth
{
    public class Season
    {
        public int Value { get; set; } = (int)SeasonType.DefaultSeason;
        public double Chance { get; set; } = 100.0;
    }
}
