using System;
using System.Globalization;
using UnityEngine;

public static class PlayerPrefsHelper
{
    private static readonly String _keyMetalMineLevel = "key.metalMineLevel";
    private static readonly String _keyCrystalMineLevel = "key.crystalMineLevel";
    private static readonly String _keyDeuteriumMineLevel = "key.deuteriumMineLevel";

    private static readonly String _keyLastUpdate = "key.lastUpdate";

    private static readonly String _dateFormat = "O";

    public enum Building
    {
        metalMine,
        crystalMine,
        deuteriumMine
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

        if (lastUpdateUTCString.Equals(String.Empty))
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

    public static void SaveBuildingLevel(Building building, int level)
    {
        switch (building)
        {
            case Building.metalMine:
                PlayerPrefs.SetInt(_keyMetalMineLevel, level);
                break;
            case Building.crystalMine:
                PlayerPrefs.SetInt(_keyCrystalMineLevel, level);
                break;
            case Building.deuteriumMine:
                PlayerPrefs.SetInt(_keyDeuteriumMineLevel, level);
                break;
        }
    }

    public static int LoadBuildingLevel(Building building)
    {
        int level = 0;
        switch (building)
        {
            case Building.metalMine:
                level = PlayerPrefs.GetInt(_keyMetalMineLevel);
                break;
            case Building.crystalMine:
                level = PlayerPrefs.GetInt(_keyCrystalMineLevel);
                break;
            case Building.deuteriumMine:
                level = PlayerPrefs.GetInt(_keyDeuteriumMineLevel);
                break;
        }
        return level;
    }

}
