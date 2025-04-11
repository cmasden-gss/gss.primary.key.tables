using System;
using System.Globalization;

namespace DatabaseTransfer.Application.Extensions
{
    public static class GlobalShopDataTypeExtensions
    {
        /// <summary>
        ///     Because using "real" / "correct" format masks is too hard.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formatMask"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string value, string formatMask)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new DateTime(1980, 1, 1);
            }

            try
            {
                switch (formatMask)
                {
                    case "CCYYMMDD":
                    case "INTEGERDATE":

                        return new DateTime(int.Parse(value.Substring(0, 4)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)));

                    case "MMDDCCYY":
                        return new DateTime(int.Parse(value.Substring(4, 4)), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)));

                    case "MMDDYY":

                        return int.Parse(value.Substring(value.Length - 2)) > 80 ? new DateTime(int.Parse($"19{value.Substring(4, 2)}"), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2))) : new DateTime(int.Parse($"20{value.Substring(4, 2)}"), int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)));

                    case "YYMMDD":
                        return int.Parse(value.Substring(0, 2)) > 80 ? new DateTime(int.Parse($"19{value.Substring(0, 2)}"), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(4, 2))) : new DateTime(int.Parse($"20{value.Substring(0, 2)}"), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(4, 2)));

                    case "YYMM":
                        return int.Parse(value.Substring(0, 2)) > 80 ? new DateTime(int.Parse($"19{value.Substring(0, 2)}"), int.Parse(value.Substring(2, 2)), 1) : new DateTime(int.Parse($"20{value.Substring(0, 2)}"), int.Parse(value.Substring(2, 2)), 1);

                    case "YY":
                        return int.Parse(value.Substring(0, 2)) > 80 ? new DateTime(int.Parse($"19{value.Substring(0, 2)}"), 1, 1) : new DateTime(int.Parse($"20{value.Substring(0, 2)}"), 1, 1);

                    case "YYMMDD HHmmssfffffff":
                        return int.Parse(value.Substring(0, 2)) > 80 ? new DateTime(int.Parse($"19{value.Substring(0, 2)}"), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)), int.Parse(value.Substring(8, 2)), int.Parse(value.Substring(10, 2))) : new DateTime(int.Parse($"20{value.Substring(0, 2)}"), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(6, 2)), int.Parse(value.Substring(8, 2)), int.Parse(value.Substring(10, 2)));

                    case "DECIMALTIME":
                        return new DateTime(1900, 1, 1, int.Parse(value.Substring(0, 2)), int.Parse((int.Parse(value.Substring(value.Length - 2)) * 60).ToString().Substring(0, 2)), 0);

                    case "HHmmss":
                        return new DateTime(1900, 1, 1, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(4, 2)));

                    case "HHmm":
                        return new DateTime(1900, 1, 1, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)), 0);

                    case "HHmmssfffffff":
                    case "HHmmssff":
                    case "INTEGERTIME":

                        var integerDateTime = new DateTime(1900, 1, 1, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(4, 2)));
                        integerDateTime = integerDateTime.AddMilliseconds(int.Parse(value.Substring(6, 2)) * 10);

                        return integerDateTime;

                    case "HH.mm":

                        if (value.Length == 4)
                        {
                            return new DateTime(1900, 1, 1, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)), 0, 0, 0);
                        }

                        break;

                    case "COMP-5/PSQLDate":
                        return new DateTime(int.Parse(value.Substring(4)), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(0, 2)));

                    case "COMP-5/PSQLTime":

                        var psqlTimeDateTime = new DateTime(1900, 1, 1, int.Parse(value.Substring(6, 2)), int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(2, 2)));

                        if (value.Length >= 8)
                        {
                            psqlTimeDateTime = psqlTimeDateTime.AddMilliseconds(int.Parse(value.Substring(0, 2)) * 10);
                        }

                        return psqlTimeDateTime;

                    case "COMP-5/PSQLDateTime":

                        if (DateTime.TryParseExact(value, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture, DateTimeStyles.None, out var psqlDateDateTime))
                        {
                            return psqlDateDateTime;
                        }

                        break;

                    case "YYDDD":

                        DateTime yydddDateTime;

                        yydddDateTime = int.Parse(value.Substring(0, 2)) > 80 ? new DateTime(Convert.ToInt32($"19{value.Substring(0, 2)}"), 1, 1) : new DateTime(Convert.ToInt32($"20{value.Substring(0, 2)}"), 1, 1);

                        var days = Convert.ToInt32(value.Substring(value.Length - 3));
                        yydddDateTime = yydddDateTime.AddDays(days - 1);

                        return yydddDateTime;

                    case "9SCOMP":
                        var tempValue = (99999999 - int.Parse(value)).ToString();

                        return new DateTime(int.Parse(tempValue.Substring(0, 4)), int.Parse(tempValue.Substring(4, 2)), int.Parse(tempValue.Substring(6, 2)));

                    case "9SCOMPYEAR":

                        return new DateTime(9999 - int.Parse(value), 1, 1);

                    case "9SCOMPTIME":

                        return new DateTime(1900, 1, 1, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(4, 2)));

                    case "TIMESTAMP":
                    case "TIMESTAMPIMPLIEDDECIMIAL":
                        return new DateTime(1968, 1, 1).AddMinutes(value.Length == 12 ? double.Parse($"{value.Substring(0, 8)}.{value.Substring(value.Length - 4)}") : double.Parse(value));
                }
            }
            catch (Exception ex)
            {
            }

            return new DateTime(1980, 1, 1);
        }
    }
}