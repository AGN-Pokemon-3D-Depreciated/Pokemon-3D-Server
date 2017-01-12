using static Pokemon_3D_Server_Core.Collections.SeasonTypeCollection;

namespace Pokemon_3D_Server_Core.Settings.Server.Game.World.SeasonMonth
{
    public class Season
    {
        public int Value { get; set; } = (int)SeasonType.DefaultSeason;
        public double Chance { get; set; } = 100.0;
    }
}
