using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Core.Extensions
{
    public static class FormatExtensions
    {
        public static string GetValueByKey(this Dictionary<string, string> parameters, string key)
        {
            return parameters.Where(f => f.Key == key).FirstOrDefault().Value;
        }


        public static string GetValueByKey(this Dictionary<string, object> parameters, string key)
        {
            return parameters.Where(f => f.Key == key).FirstOrDefault().Value.ToString();
        }


        public static string ToGibDocumentDate(this string date)
        {
            DateTime dateTime = DateTime.Parse(date);
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static decimal ToDecimal(this string value)
        {
            return decimal.Parse(value.Replace(",", ".").ToString(), CultureInfo.InvariantCulture);
        }

        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }


        public static string ToCombineString(this string value)
        {
            return value.Replace(" ", "");
        }

        public static string ToFixPhone(this string value)
        {
            return "0" + value.Replace("(", "").Replace(")", "").Replace("-", "");
        }

        public static string ToOracleDecimalFormat(this decimal value)
        {
            if (value == 0)
            {
                return "0,00";
            }
            else
            {
                return value.ToString("#.##").Replace(".", ",");
            }
        }

        public static int BooleanToInt(this bool value)
        {
            return value == true ? 1 : 0;
        }

        public static bool IntToBolean(this int value)
        {
            return value == 0 ? false : true;
        }
    }
}
