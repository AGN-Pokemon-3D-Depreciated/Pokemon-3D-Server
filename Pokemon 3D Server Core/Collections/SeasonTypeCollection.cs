namespace Pokemon_3D_Server_Core.Collections
{
    public class SeasonTypeCollection
    {
        public enum SeasonType
        {
            Winter = 0,
            Spring = 1,
            Summer = 2,
            Fall = 3,

            Random = -1,
            DefaultSeason = -2,
            Custom = -3,
            Nothing = -4,
        }
    }
}
