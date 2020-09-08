using System;
using System.Globalization;
using UnityEngine;

public static class PlayerPrefsHelper
{
    private static readonly String _keyMetalMineLevel = "key.metalMineLevel";
    private static readonly String _keyCrystalMineLevel = "key.crystalMineLevel";
    private static readonly String _keyDeuteriumMineLevel = "key.deuteriumMineLevel";

    private static readonly String _keyLastUpdate = "key.lastUpdate";

    public enum Building
    {
        metalMine,
        crystalMine,
        deuteriumMine
    }

    public static void SaveLastUpdateDate()
    {
        String nowString = DateHelper.ToUniversalDateString(DateTime.Now);
        PlayerPrefs.SetString(_keyLastUpdate, nowString);
    }

    public static DateTime? LoadLastUpdateDate()
    {
        String lastUpdateUTCString = PlayerPrefs.GetString(_keyLastUpdate);

        if (lastUpdateUTCString.Equals(String.Empty))
        {
            Logger.Warning("Could not read lastUpdate. Probably never saved before.");
            return null;
        }

        DateTime? lastUpdateDate = DateHelper.UniversalDateFromString(lastUpdateUTCString);
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
        Logger.Debug("Saved building " + building + " with level " + level + ".");
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
