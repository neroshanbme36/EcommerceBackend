using System.Globalization;
using Application.Helpers;

namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string value)
        {
            return MathHelper.IsNumeric(value);
        }

        public static string ToTitleCase(this string input)
        {
            return new CultureInfo("en-US", false).TextInfo.ToTitleCase(input.ToLower());
        }

        public static bool ToBoolean(this string value)
        {
            return value.Equals("1") ? true : false;
        }

        public static int ToInt(this string value)
        {
            int result = 0;
            if (value.IsNumeric()) int.TryParse(value, out result);
            return result;
        }

        public static decimal ToDecimal(this string value)
        {
            decimal result = 0.00m;
            if (value.IsNumeric()) decimal.TryParse(value, out result);
            return result;
        }
    }
}