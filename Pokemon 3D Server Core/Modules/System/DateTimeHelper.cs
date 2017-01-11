using System;

namespace Modules.System
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Convert <see cref="DateTime"/> to UnixTime.
        /// </summary>
        /// <param name="time">DateTime to convert.</param>
        public static long ToUnixTime(this DateTime time)
        {
            return (time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds.Truncate().ToString().ToLong();
        }

        /// <summary>
        /// Convert Unix Time to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="unitTime">Unix Time to convert.</param>
        public static DateTime ToDateTime(this long unitTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unitTime).ToLocalTime();
        }
    }
}