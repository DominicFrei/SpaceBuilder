using System;
using System.Globalization;
using UnityEngine;

public static class PlayerPrefsHelper
{
    private static readonly String _keyMetalMineLevel = "key.metalMineLevel";
    private static readonly String _keyCrystalMineLevel = "key.crystalMineLevel";
    private static readonly String _keyDeuteriumMineLevel = "key.deuteriumMineLevel";

    public enum Building
    {
        metalMine,
        crystalMine,
        deuteriumMine
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
