using static Pokemon_3D_Server_Core.Settings.Server.World.World;

namespace Pokemon_3D_Server_Core.Settings.Server.World.SeasonMonth
{
    public class Season
    {
        /// <summary>
        /// Get/Set Season value.
        /// </summary>
        public int Value { get; set; } = (int)SeasonType.DefaultSeason;

        /// <summary>
        /// Get/Set Chance.
        /// </summary>
        public double Chance { get; set; } = 100.0;
    }
}
