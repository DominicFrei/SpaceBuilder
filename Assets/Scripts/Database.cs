using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{
    #region Private Fields
    private static readonly string _savePathResources = Application.persistentDataPath + "/resources.bin";
    private static readonly string _savePathBuildings = Application.persistentDataPath + "/buildings.bin";
    #endregion

    #region Resources
    public static void SaveResources()
    {
        string lastUpdateString = DateHelper.ToUniversalDateString(DateTime.Now);
        Resources.Instance.LastUpdate = lastUpdateString;
        Database.SaveEntity<Resources>(Resources.Instance, _savePathResources);
        Logger.Debug("Saved resources: " + Resources.Instance.Metal + " / " + Resources.Instance.Crystal + " / " + Resources.Instance.Deuterium + " / " + lastUpdateString);
    }

    public static void LoadResources()
    {
        Resources resources;

        try
        {
            resources = LoadEntity<Resources>(_savePathResources);
        }
        catch (Exception exception)
        {
            Logger.Error("Exception while loading resources: " + exception);
            return;
        }

        DateTime lastUpdate = DateHelper.UniversalDateFromString(Resources.Instance.LastUpdate) ?? DateTime.Now;

        Logger.Debug("Resources loaded: " + Resources.Instance.Metal + " / " + Resources.Instance.Crystal + " / " + Resources.Instance.Deuterium);
    }
    #endregion

    #region Buildings
    public static void SaveBuildings(BuildingsEntity buildingsEntity)
    {
        SaveEntity<BuildingsEntity>(buildingsEntity, _savePathBuildings);
        Logger.Debug("Saved buildings: " + buildingsEntity);
    }

    public static BuildingsEntity LoadBuildings()
    {
        return LoadEntity<BuildingsEntity>(_savePathBuildings);
    }
    #endregion

    #region Private Functions
    private static void SaveEntity<T>(T entity, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Create);

        try
        {
            binaryFormatter.Serialize(fileStream, entity);
        }
        catch (Exception exception)
        {
            Logger.Error("Error during serialization: " + exception);
        }
        finally
        {
            fileStream.Close();
        }
        Logger.Info("Saved data to: " + path);
    }

    private static T LoadEntity<T>(string path)
    {
        if (!File.Exists(path))
        {
            Logger.Error("Could not find file at path: " + path);
            throw new FileNotFoundException();
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Open);
        T entity;
        try
        {
            entity = (T)binaryFormatter.Deserialize(fileStream);
        }
        catch (Exception exception)
        {
            Logger.Error("Error during deserialization: " + exception);
            throw;
        }
        finally
        {
            fileStream.Close();
        }

        return entity;
    }
    #endregion

}
