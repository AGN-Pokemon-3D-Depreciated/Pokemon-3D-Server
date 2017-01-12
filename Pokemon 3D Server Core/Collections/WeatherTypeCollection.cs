namespace Pokemon_3D_Server_Core.Collections
{
    public class WeatherTypeCollection
    {
        public enum WeatherType
        {
            Clear = 0,
            Rain = 1,
            Snow = 2,
            Underwater = 3,
            Sunny = 4,
            Fog = 5,
            Thunderstorm = 6,
            Sandstorm = 7,
            Ash = 8,
            Blizzard = 9,

            Random = -1,
            DefaultWeather = -2,
            Custom = -3,
            Real = -4,
            Nothing = -5,
        }
    }
}
