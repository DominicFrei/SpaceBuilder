using System;
using System.Globalization;
using UnityEngine;

public static class DateHelper
{
    private static readonly String _dateFormat = "O";
    public static int DifferenceToNowInSeconds(DateTime? datetime)
    {
        return DifferenceInSeconds(datetime, DateTime.Now.ToUniversalTime());
    }

    public static int DifferenceInSeconds(DateTime? date1, DateTime? date2)
    {
        if (null == date1 || null == date2)
        {
            Logger.Warning("At least one date was null.");
            return 0;
        }

        DateTime date1Value = date1 ?? DateTime.Now;
        DateTime date2Value = date2 ?? DateTime.Now;

        TimeSpan difference = date1Value - date2Value;
        TimeSpan unsignedDifference = difference.Duration();
        int differenceInSeconds = (int)unsignedDifference.TotalSeconds;
        return differenceInSeconds;
    }

    public static string ToUniversalDateString(DateTime date)
    {
        DateTime nowUTC = date.ToUniversalTime();
        String nowUTCString = nowUTC.ToString(_dateFormat, CultureInfo.InvariantCulture);
        return nowUTCString;
    }

    public static DateTime? UniversalDateFromString(string dateString)
    {
        TimeZoneInfo.ClearCachedData(); // just in case the time zone has changed
        bool dateParsedSuccessfully = DateTime.TryParseExact(dateString, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime lastUpdateDate);

        if (!dateParsedSuccessfully)
        {
            Logger.Warning("Could not parse saved date. Maybe never saved before.");
            return null;
        }

        return lastUpdateDate;
    }
}
