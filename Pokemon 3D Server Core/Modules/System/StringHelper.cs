using System;

namespace Modules.System
{
    public static class StringHelper
    {
        /// <summary>
        /// Splits a string into substrings that are based on the character and gets a 32-bit integer that represents the total number of elements in string array.
        /// </summary>
        /// <param name="fullString">String to split.</param>
        /// <param name="seperator">A character that delimits the substrings in this string.</param>
        public static int SplitCount(this string fullString, char seperator = '|')
        {
            return fullString.Contains(seperator.ToString()) ? fullString.Split(seperator).Length : 1;
        }

        /// <summary>
        /// Splits a string into substrings that are based on the character and gets a string object in the string array.
        /// </summary>
        /// <param name="fullString">String to split.</param>
        /// <param name="valueIndex">A zero-index based value to get a string object in the string array.</param>
        /// <param name="seperator">A character that delimits the substrings in this string.</param>
        public static string GetSplit(this string fullString, int valueIndex, char seperator = '|')
        {
            int expectedSplitCount = fullString.SplitCount(seperator);

            if (expectedSplitCount == 1)
                return fullString;
            else if (valueIndex < expectedSplitCount)
                return fullString.Split(seperator)[valueIndex];
            else
                return fullString.Split(seperator)[expectedSplitCount - 1];
        }

        /// <summary>
        /// Determines whether two String objects have the same value.
        /// </summary>
        /// <param name="a">The first string to compare, or null.</param>
        /// <param name="b">The second string to compare, or null.</param>
        public static new bool Equals(this object a, object b)
        {
            if (string.Equals(a, b))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Determines whether two String objects have the same value.
        /// </summary>
        /// <param name="a">The first string to compare, or null.</param>
        /// <param name="b">The second string to compare, or null.</param>
        public static bool Equals(this string a, params string[] b)
        {
            foreach (string item in b)
            {
                if (string.Equals(a, item, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}