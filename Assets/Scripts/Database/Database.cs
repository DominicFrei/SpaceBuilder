using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{
    private static readonly string _savePathResources = Application.persistentDataPath + "/resources.bin";
    private static readonly string _savePathMetalMine = Application.persistentDataPath + "/metalMine.bin";
    private static readonly string _savePathCrystalMine = Application.persistentDataPath + "/crystalMine.bin";
    private static readonly string _savePathDeuteriumMine = Application.persistentDataPath + "/deuteriumMine.bin";

    public static void SaveResources(int metal, int crystal, int deuterium, DateTime lastUpdate)
    {
        string lastUpdateString = DateHelper.ToUniversalDateString(lastUpdate);
        ResourcesEntity resourcesEntity = new ResourcesEntity(metal, crystal, deuterium, lastUpdateString);
        Database.SaveEntity<ResourcesEntity>(resourcesEntity, _savePathResources);
        Logger.Debug("Saved resources: " + metal + " / " + crystal + " / " + deuterium + " / " + lastUpdateString);
    }

    public static (int metal, int crystal, int deuterium, DateTime lastUpdate) LoadResources()
    {
        ResourcesEntity resourcesEntity;

        try
        {
            resourcesEntity = LoadEntity<ResourcesEntity>(_savePathResources);
        }
        catch (Exception exception)
        {
            Logger.Error("Exception while loading resources: " + exception);
            return (0, 0, 0, DateTime.Now);
        }

        DateTime lastUpdate = DateHelper.UniversalDateFromString(resourcesEntity.LastUpdate) ?? DateTime.Now;

        return (resourcesEntity.Metal, resourcesEntity.Crystal, resourcesEntity.Deuterium, lastUpdate);
    }

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
        Logger.Info("Saved data to: " + _savePathResources);
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

}
