using System;
using UnityEngine;

public class DateHelper
{
    public static int DifferenceToNowInSeconds(DateTime? datetime)
    {
        return DifferenceInSeconds(datetime, DateTime.Now.ToUniversalTime());
    }

    public static int DifferenceInSeconds(DateTime? date1, DateTime? date2)
    {
        if (null == date1 || null == date2)
        {
            Debug.LogWarning("At least one date was null.");
            return 0;
        }

        DateTime date1Value = date1 ?? DateTime.Now;
        DateTime date2Value = date2 ?? DateTime.Now;

        TimeSpan difference = date1Value - date2Value;
        TimeSpan unsignedDifference = difference.Duration();
        int differenceInSeconds = (int)unsignedDifference.TotalSeconds;
        return differenceInSeconds;
    }
}
