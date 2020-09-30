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
        ResourcesEntity resourcesEntity = new ResourcesEntity(Resources.Instance.Metal, Resources.Instance.Crystal, Resources.Instance.Deuterium, lastUpdateString);
        Database.SaveEntity<ResourcesEntity>(resourcesEntity, _savePathResources);
        Logger.Info("Saved resources: " + Resources.Instance.Metal + " / " + Resources.Instance.Crystal + " / " + Resources.Instance.Deuterium);
        Logger.Info("lastUpdateString (" + lastUpdateString + ")");
    }

    public static void LoadResources()
    {
        ResourcesEntity resourcesEntity;

        try
        {
            resourcesEntity = LoadEntity<ResourcesEntity>(_savePathResources);
        }
        catch (Exception exception)
        {
            Logger.Error("Exception while loading resources: " + exception);
            return;
        }

        DateTime lastUpdate = DateHelper.UniversalDateFromString(resourcesEntity.LastUpdate) ?? DateTime.Now;

        Resources.Instance.Metal = resourcesEntity.Metal;
        Resources.Instance.Crystal = resourcesEntity.Crystal;
        Resources.Instance.Deuterium = resourcesEntity.Deuterium;
        Resources.Instance.LastUpdate = lastUpdate;

        Logger.Info("Resources loaded: " + Resources.Instance.Metal + " / " + Resources.Instance.Crystal + " / " + Resources.Instance.Deuterium);
        Logger.Info("resources.LastUpdate (" + resourcesEntity.LastUpdate + ") --> Resources.Instance.LastUpdate (" + Resources.Instance.LastUpdate + ")");
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
