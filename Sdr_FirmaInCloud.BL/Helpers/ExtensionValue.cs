using System;

namespace SDR_FirmaInCloud.BL
{
    public static class ExtensionValue
    {
        public static object ToIntOrDbNull(this string value)
        {
            int? val = value.ToInt();

            if (val == null) return DBNull.Value;

            return val;
        }

        public static int? ToInt(this string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            return int.Parse(value, new System.Globalization.CultureInfo("it-IT", false));
        }

        public static double? ToDouble(this string value, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return double.Parse(value, culture);
        }

        public static object ToDoubleOrDbNull(this string value, System.Globalization.CultureInfo culture)
        {
            double? val = value.ToDouble(culture);

            if (val == null)
                return DBNull.Value;

            return val;
        }

        public static string StringOrNull(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}

