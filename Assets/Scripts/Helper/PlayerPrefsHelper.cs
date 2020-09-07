using System;
using System.Globalization;
using UnityEngine;

public class PlayerPrefsHelper
{
    private static readonly String _keyMetal = "key.metal";
    private static readonly String _keyCrystal = "key.crystal";
    private static readonly String _keyDeuterium = "key.deuterium";
    private static readonly String _keyLastUpdate = "key.lastUpdate";
    private static readonly String _dateFormat = "O";

    enum Keys
    {
        foo
    }

    public static void SaveResources(int metal, int crystal, int deuterium)
    {
        PlayerPrefs.SetInt(_keyMetal, metal);
        PlayerPrefs.SetInt(_keyCrystal, crystal);
        PlayerPrefs.SetInt(_keyDeuterium, deuterium);
        Debug.Log("foooo:" + Keys.foo.ToString());
    }

    public static (int metal, int crystal, int deuterium) LoadResources()
    {
        int metal = PlayerPrefs.GetInt(_keyMetal);
        int crystal = PlayerPrefs.GetInt(_keyCrystal);
        int deuterium = PlayerPrefs.GetInt(_keyDeuterium);
        return (metal, crystal, deuterium);
    }

    public static void SaveLastUpdateDate()
    {
        DateTime now = DateTime.Now;
        DateTime nowUTC = now.ToUniversalTime();
        String nowUTCString = nowUTC.ToString(_dateFormat, CultureInfo.InvariantCulture);
        PlayerPrefs.SetString(_keyLastUpdate, nowUTCString);
    }

    public static DateTime? LoadLastUpdateDate()
    {
        String lastUpdateUTCString = PlayerPrefs.GetString(_keyLastUpdate);

        if (lastUpdateUTCString.Equals(""))
        {
            Debug.LogWarning("Could not read lastUpdate. Probably never saved before.");
            return null;
        }

        TimeZoneInfo.ClearCachedData(); // just in case the time zone has changed
        bool dateParsedSuccessfully = DateTime.TryParseExact(lastUpdateUTCString, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime lastUpdateDate);

        if (!dateParsedSuccessfully)
        {
            Debug.LogError("Could not parse saved date.");
            return null;
        }

        return lastUpdateDate;
    }

}
