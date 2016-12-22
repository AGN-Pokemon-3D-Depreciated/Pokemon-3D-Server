using System;

namespace Modules.System
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Convert <see cref="DateTime"/> to UnixTime.
        /// </summary>
        /// <param name="Time">DateTime to convert.</param>
        public static long ToUnixTime(this DateTime Time)
        {
            return (Time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds.Truncate().ToString().ToLong();
        }

        /// <summary>
        /// Convert Unix Time to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="UnixTime">Unix Time to convert.</param>
        public static DateTime ToDateTime(this long UnixTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(UnixTime).ToLocalTime();
        }
    }
}