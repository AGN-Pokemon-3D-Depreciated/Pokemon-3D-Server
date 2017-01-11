using System;
using System.Globalization;

namespace Modules.System
{
    public static class MathHelper
    {
        private static readonly string CurrentCulture = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        /// <summary>
        /// Represents the natural logarithmic base, specified by the constant, e.
        /// </summary>
        public const double E = Math.E;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        /// </summary>
        public const double PI = Math.PI;

        /// <summary>
        /// Returns the absolute value of a Decimal number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to Decimal.MinValue, but less than or equal to Decimal.MaxValue.</param>
        public static decimal Abs(this decimal value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to Double.MinValue, but less than or equal to Double.MaxValue.</param>
        public static double Abs(this double value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of a single-precision floating-point number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to Single.MinValue, but less than or equal to Single.MaxValue.</param>
        public static float Abs(this float value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A number that is greater than Int32.MinValue, but less than or equal to Int32.MaxValue.</param>
        public static int Abs(this int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A number that is greater than Int64.MinValue, but less than or equal to Int64.MaxValue.</param>
        public static long Abs(this long value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of an 8-bit signed integer.
        /// </summary>
        /// <param name="value">A number that is greater than SByte.MinValue, but less than or equal to SByte.MaxValue.</param>
        public static sbyte Abs(this sbyte value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of a 16-bit signed integer.
        /// </summary>
        /// <param name="value">A number that is greater than Int16.MinValue, but less than or equal to Int16.MaxValue.</param>
        public static short Abs(this short value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a cosine, where d must be greater than or equal to -1, but less than or equal to 1.</param>
        public static double Acos(this double d)
        {
            return Math.Acos(d);
        }

        /// <summary>
        /// Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="d">A number representing a sine, where d must be greater than or equal to -1, but less than or equal to 1.</param>
        public static double Asin(this double d)
        {
            return Math.Asin(d);
        }

        /// <summary>
        /// Returns the angle whose tangent is the specified number.
        /// </summary>
        /// <param name="d">A number representing a tangent.</param>
        public static double Atan(this double d)
        {
            return Math.Atan(d);
        }

        /// <summary>
        /// Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y">The y coordinate of a point.</param>
        /// <param name="x">The x coordinate of a point.</param>
        public static double Atan2(this double y, double x)
        {
            return Math.Atan2(y, x);
        }

        /// <summary>
        /// Produces the full product of two 32-bit numbers.
        /// </summary>
        /// <param name="a">The first number to multiply.</param>
        /// <param name="b">The second number to multiply.</param>
        public static long BigMul(this int a, int b)
        {
            return Math.BigMul(a, b);
        }

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the specified decimal number.
        /// </summary>
        /// <param name="d">A decimal number.</param>
        public static decimal Ceiling(this decimal d)
        {
            return Math.Ceiling(d);
        }

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the specified double-precision floating-point number.
        /// </summary>
        /// <param name="a">A double-precision floating-point number.</param>
        public static double Ceiling(this double a)
        {
            return Math.Ceiling(a);
        }

        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="d">An angle, measured in radians.</param>
        public static double Cos(this double d)
        {
            return Math.Cos(d);
        }

        /// <summary>
        /// Returns the hyperbolic cosine of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        public static double Cosh(this double value)
        {
            return Math.Cosh(value);
        }

        /// <summary>
        /// Calculates the quotient of two 32-bit signed integers and also returns the remainder in an output parameter.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <param name="result">The remainder.</param>
        public static int DivRem(this int a, int b, out int result)
        {
            Math.DivRem(a, b, out result);
            return result;
        }

        /// <summary>
        /// Calculates the quotient of two 64-bit signed integers and also returns the remainder in an output parameter.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <param name="result">The remainder.</param>
        public static long DivRem(this long a, long b, out long result)
        {
            Math.DivRem(a, b, out result);
            return result;
        }

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="d">A number specifying a power.</param>
        public static double Exp(this double d)
        {
            return Math.Exp(d);
        }

        /// <summary>
        /// Returns the largest integer less than or equal to the specified decimal number.
        /// </summary>
        /// <param name="d">A decimal number.</param>
        public static decimal Floor(this decimal d)
        {
            return Math.Floor(d);
        }

        /// <summary>
        /// Returns the largest integer less than or equal to the specified double-precision floating-point number.
        /// </summary>
        /// <param name="d">A double-precision floating-point number.</param>
        public static double Floor(this double d)
        {
            return Math.Floor(d);
        }

        /// <summary>
        /// Returns the remainder resulting from the division of a specified number by another specified number.
        /// </summary>
        /// <param name="x">A dividend.</param>
        /// <param name="y">A divisor.</param>
        public static double IEEERemainder(this double x, double y)
        {
            return Math.IEEERemainder(x, y);
        }

        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="d">The number whose logarithm is to be found.</param>
        public static double Log(this double d)
        {
            return Math.Log(d);
        }

        /// <summary>
        /// Returns the logarithm of a specified number in a specified base.
        /// </summary>
        /// <param name="a">The number whose logarithm is to be found.</param>
        /// <param name="newBase">The base of the logarithm.</param>
        public static double Log(this double a, double newBase)
        {
            return Math.Log(a, newBase);
        }

        /// <summary>
        /// Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="d">The number whose logarithm is to be found.</param>
        public static double Log10(this double d)
        {
            return Math.Log10(d);
        }

        /// <summary>
        /// Returns the larger of two 8-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 8-bit unsigned integers to compare.</param>
        public static byte Max(this byte val1, byte val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two decimal numbers.
        /// </summary>
        /// <param name="val1">The first of two decimal numbers to compare.</param>
        /// <param name="val2">The second of two decimal numbers to compare.</param>
        public static decimal Max(this decimal val1, decimal val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two double-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
        public static double Max(this double val1, double val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two single-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two single-precision floating-point numbers to compare.</param>
        public static float Max(this float val1, float val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two 32-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 32-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 32-bit signed integers to compare.</param>
        public static int Max(this int val1, int val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two 64-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 64-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 64-bit signed integers to compare.</param>
        public static long Max(this long val1, long val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two 8-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 8-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 8-bit signed integers to compare.</param>
        public static sbyte Max(this sbyte val1, sbyte val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two 16-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 16-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 16-bit signed integers to compare.</param>
        public static short Max(this short val1, short val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two 32-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 32-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 32-bit unsigned integers to compare.</param>
        public static uint Max(this uint val1, uint val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two 64-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 64-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 64-bit unsigned integers to compare.</param>
        public static ulong Max(this ulong val1, ulong val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the larger of two 16-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 16-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 16-bit unsigned integers to compare.</param>
        public static ushort Max(this ushort val1, ushort val2)
        {
            return Math.Max(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two 8-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 8-bit unsigned integers to compare.</param>
        public static byte Min(this byte val1, byte val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two decimal numbers.
        /// </summary>
        /// <param name="val1">The first of two decimal numbers to compare.</param>
        /// <param name="val2">The second of two decimal numbers to compare.</param>
        public static decimal Min(this decimal val1, decimal val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two double-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
        public static double Min(this double val1, double val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two single-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two single-precision floating-point numbers to compare.</param>
        public static float Min(this float val1, float val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two 32-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 32-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 32-bit signed integers to compare.</param>
        public static int Min(this int val1, int val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two 64-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 64-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 64-bit signed integers to compare.</param>
        public static long Min(this long val1, long val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two 8-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 8-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 8-bit signed integers to compare.</param>
        public static sbyte Min(this sbyte val1, sbyte val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two 16-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 16-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 16-bit signed integers to compare.</param>
        public static short Min(this short val1, short val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two 32-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 32-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 32-bit unsigned integers to compare.</param>
        public static uint Min(this uint val1, uint val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two 64-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 64-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 64-bit unsigned integers to compare.</param>
        public static ulong Min(this ulong val1, ulong val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns the smaller of two 16-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 16-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 16-bit unsigned integers to compare.</param>
        public static ushort Min(this ushort val1, ushort val2)
        {
            return Math.Min(val1, val2);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x">A double-precision floating-point number to be raised to a power.</param>
        /// <param name="y">A double-precision floating-point number that specifies a power.</param>
        public static double Pow(this double x, double y)
        {
            return Math.Pow(x, y);
        }

        /// <summary>
        /// Rounds a decimal value to the nearest integral value.
        /// </summary>
        /// <param name="d">A decimal number to be rounded.</param>
        public static decimal Round(this decimal d)
        {
            return Math.Round(d);
        }

        /// <summary>
        /// Rounds a double-precision floating-point value to the nearest integral value.
        /// </summary>
        /// <param name="d">A double-precision floating-point number to be rounded.</param>
        public static double Round(this double d)
        {
            return Math.Round(d);
        }

        /// <summary>
        /// Rounds a decimal value to a specified number of fractional digits.
        /// </summary>
        /// <param name="d">A decimal number to be rounded.</param>
        /// <param name="decimals">The number of decimal places in the return value.</param>
        public static decimal Round(this decimal d, int decimals)
        {
            return Math.Round(d, decimals);
        }

        /// <summary>
        /// Rounds a decimal value to the nearest integer. A parameter specifies how to round the value if it is midway between two numbers.
        /// </summary>
        /// <param name="d">A decimal number to be rounded.</param>
        /// <param name="mode">Specification for how to round d if it is midway between two other numbers.</param>
        public static decimal Round(this decimal d, MidpointRounding mode)
        {
            return Math.Round(d, mode);
        }

        /// <summary>
        /// Rounds a double-precision floating-point value to a specified number of fractional digits.
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The number of decimal places in the return value.</param>
        public static double Round(this double value, int digits)
        {
            return Math.Round(value, digits);
        }

        /// <summary>
        /// Rounds a double-precision floating-point value to the nearest integer. A parameter specifies how to round the value if it is midway between two numbers.
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="mode">Specification for how to round d if it is midway between two other numbers.</param>
        public static double Round(this double value, MidpointRounding mode)
        {
            return Math.Round(value, mode);
        }

        /// <summary>
        /// Rounds a decimal value to a specified number of fractional digits. A parameter specifies how to round the value if it is midway between two numbers.
        /// </summary>
        /// <param name="d">A decimal number to be rounded.</param>
        /// <param name="decimals">The number of decimal places in the return value.</param>
        /// <param name="mode">Specification for how to round d if it is midway between two other numbers.</param>
        public static decimal Round(this decimal d, int decimals, MidpointRounding mode)
        {
            return Math.Round(d, decimals, mode);
        }

        /// <summary>
        /// Rounds a decimal value to a specified number of fractional digits. A parameter specifies how to round the value if it is midway between two numbers.
        /// </summary>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="digits">The number of fractional digits in the return value.</param>
        /// <param name="mode">Specification for how to round d if it is midway between two other numbers.</param>
        public static double Round(this double value, int digits, MidpointRounding mode)
        {
            return Math.Round(value, digits, mode);
        }

        /// <summary>
        /// Returns a value indicating the sign of a decimal number.
        /// </summary>
        /// <param name="value">A signed decimal number.</param>
        public static int Sign(this decimal value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns a value indicating the sign of a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A signed number.</param>
        public static int Sign(this double value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns a value indicating the sign of a single-precision floating-point number.
        /// </summary>
        /// <param name="value">A signed number.</param>
        public static int Sign(this float value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns a value indicating the sign of a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A signed number.</param>
        public static int Sign(this int value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns a value indicating the sign of a 64-bit signed integer.
        /// </summary>
        /// <param name="value">A signed number.</param>
        public static int Sign(this long value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns a value indicating the sign of an 8-bit signed integer.
        /// </summary>
        /// <param name="value">A signed number.</param>
        public static int Sign(this sbyte value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns a value indicating the sign of a 16-bit signed integer.
        /// </summary>
        /// <param name="value">A signed number.</param>
        public static int Sign(this short value)
        {
            return Math.Sign(value);
        }

        /// <summary>
        /// Returns the sine of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        public static double Sin(this double a)
        {
            return Math.Sin(a);
        }

        /// <summary>
        /// Returns the hyperbolic sine of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        public static double Sinh(this double value)
        {
            return Math.Sinh(value);
        }

        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <param name="d">The number whose square root is to be found.</param>
        public static double Sqrt(this double d)
        {
            return Math.Sqrt(d);
        }

        /// <summary>
        /// Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="a">An angle, measured in radians.</param>
        public static double Tan(this double a)
        {
            return Math.Tan(a);
        }

        /// <summary>
        /// Returns the hyperbolic tangent of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        public static double Tanh(this double value)
        {
            return Math.Tanh(value);
        }

        /// <summary>
        /// Calculates the integral part of a specified decimal number.
        /// </summary>
        /// <param name="d">A number to truncate.</param>
        public static decimal Truncate(this decimal d)
        {
            return Math.Truncate(d);
        }

        /// <summary>
        /// Calculates the integral part of a specified double-precision floating-point number.
        /// </summary>
        /// <param name="d">A number to truncate.</param>
        public static double Truncate(this double d)
        {
            return Math.Truncate(d);
        }

        /// <summary>
        /// Convert Math Value to current culture.
        /// </summary>
        /// <param name="value">The value to convert in string.</param>
        /// <param name="separator">The separator used in this culture.</param>
        public static string ConvertStringCulture(this string value, string separator = null)
        {
            if (separator == null)
                return value.Replace(".", CurrentCulture).Replace(",", CurrentCulture);
            else
                return value.Replace(".", separator).Replace(",", separator);
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="byte"/> equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert. The string is interpreted using the <see cref="NumberStyles.Integer"/> style.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static byte ToByte(this string value, byte defaultValue = 0)
        {
            try { return byte.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="decimal"/> equivalent.
        /// </summary>
        /// <param name="value">The string representation of the number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static decimal ToDecimal(this string value, decimal defaultValue = 0.0m)
        {
            try { return decimal.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its double-precision floating-point number equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static double ToDouble(this string value, double defaultValue = 0.0)
        {
            try { return double.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the single-precision floating-point representation of a number to its double-precision floating-point number equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static double ToDouble(this float value, double defaultValue = 0.0)
        {
            try { return double.Parse(value.ToString().ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its single-precision floating-point number equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static float ToFloat(this string value, float defaultValue = 0.0f)
        {
            try { return float.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the double-precision floating-point representation of a number to its single-precision floating-point number equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static float ToFloat(this double value, float defaultValue = 0.0f)
        {
            try { return float.Parse(value > float.MaxValue ? float.MaxValue.ToString() : value.ToString().ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its single-precision floating-point number equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static float ToSingle(this string value, float defaultValue = 0.0f)
        {
            try { return float.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the double-precision floating-point representation of a number to its single-precision floating-point number equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static float ToSingle(this double value, float defaultValue = 0.0f)
        {
            try { return float.Parse(value > float.MaxValue ? float.MaxValue.ToString() : value.ToString().ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static int ToInt(this string value, int defaultValue = 0)
        {
            try { return int.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the single-precision floating-point representation of a number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static int ToInt(this float value, int defaultValue = 0)
        {
            try { return int.Parse(value > int.MaxValue ? int.MaxValue.ToString() : value.ToDouble().Round(0).ToString()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the double-precision floating-point representation of a number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static int ToInt(this double value, int defaultValue = 0)
        {
            try { return int.Parse(value > int.MaxValue ? int.MaxValue.ToString() : value.Round(0).ToString()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the 64-bit signed integer representation of a number to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static int ToInt(this long value, int defaultValue = 0)
        {
            try { return int.Parse(value > int.MaxValue ? int.MaxValue.ToString() : value.ToString()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its 64-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static long ToLong(this string value, long defaultValue = 0)
        {
            try { return long.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the 32-bit signed integer representation of a number to its 64-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static long ToLong(this int value, long defaultValue = 0)
        {
            try { return long.Parse(value.ToString()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its 8-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">A string that represents a number to convert. The string is interpreted using the <see cref="NumberStyles.Integer"/> style.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static sbyte ToSbyte(this string value, sbyte defaultValue = 0)
        {
            try { return sbyte.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its 16-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">A string containing a number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static short ToShort(this string value, short defaultValue = 0)
        {
            try { return short.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its 32-bit unsigned integer equivalent.
        /// </summary>
        /// <param name="value">A string representing the number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static uint ToUint(this string value, uint defaultValue = 0)
        {
            try { return uint.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its 64-bit unsigned integer equivalent.
        /// </summary>
        /// <param name="value">A string that represents the number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static ulong ToUlong(this string value, ulong defaultValue = 0)
        {
            try { return ulong.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Converts the string representation of a number to its 16-bit unsigned integer equivalent.
        /// </summary>
        /// <param name="value">A string that represents the number to convert.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static ushort ToUshort(this string value, ushort defaultValue = 0)
        {
            try { return ushort.Parse(value.ConvertStringCulture()); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Convert string to bool type.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        public static bool ToBool(this string value, bool defaultValue = true)
        {
            try
            {
                if (int.Parse(value.ConvertStringCulture()) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                if (string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (string.Equals(value, "false", StringComparison.OrdinalIgnoreCase))
                    return false;
                else
                    return defaultValue;
            }
        }

        /// <summary>
        /// Convert integer to bool type.
        /// </summary>
        /// <param name="value">The int value to convert.</param>
        public static bool ToBool(this int value)
        {
            if (value > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Convert boolean to integer type.
        /// </summary>
        /// <param name="value">The bool value to convert.</param>
        public static int ToInt(this bool value)
        {
            if (value)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <param name="defaultValue">The default value returned if the conversion fails.</param>
        public static int Random(int minValue, int maxValue, int defaultValue = 0)
        {
            try { return new Random().Next(minValue, maxValue); }
            catch (Exception) { return defaultValue; }
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static byte Clamp(this byte value, byte minValue, byte maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static decimal Clamp(this decimal value, decimal minValue, decimal maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static double Clamp(this double value, double minValue, double maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static float Clamp(this float value, float minValue, float maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static int Clamp(this int value, int minValue, int maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static long Clamp(this long value, long minValue, long maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static sbyte Clamp(this sbyte value, sbyte minValue, sbyte maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static short Clamp(this short value, short minValue, short maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static uint Clamp(this uint value, uint minValue, uint maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static ulong Clamp(this ulong value, ulong minValue, ulong maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// Clamp the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static ushort Clamp(this ushort value, ushort minValue, ushort maxValue)
        {
            if (value < minValue)
                return minValue;
            else if (value > maxValue)
                return maxValue;
            else
                return value;
        }

        /// <summary>
        /// RollOver the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to rollover.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static int RollOver(this int value, int minValue, int maxValue)
        {
            int diff = maxValue - minValue + 1;
            int newValue = value;

            if (value > maxValue)
            {
                while (newValue > maxValue)
                    newValue -= diff;
            }
            else if (value < minValue)
            {
                while (newValue < maxValue)
                    newValue += diff;
            }

            return newValue;
        }

        /// <summary>
        /// RollOver the value between the minValue and the maxValue.
        /// </summary>
        /// <param name="value">The value to rollover.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public static long RollOver(this long value, long minValue, long maxValue)
        {
            long diff = maxValue - minValue + 1;
            long newValue = value;

            if (value > maxValue)
            {
                while (newValue > maxValue)
                    newValue -= diff;
            }
            else if (value < minValue)
            {
                while (newValue < maxValue)
                    newValue += diff;
            }

            return newValue;
        }
    }
}