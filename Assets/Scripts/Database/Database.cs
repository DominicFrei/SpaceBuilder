using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{
    private static readonly string _savePath = Application.persistentDataPath + "/resources.bin";

    public static void SaveResources(int metal, int crystal, int deuterium, DateTime lastUpdate)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(_savePath, FileMode.Create);
        string lastUpdateString = DateHelper.ToUniversalDateString(lastUpdate);
        ResourcesEntity resourcesEntity = new ResourcesEntity(metal, crystal, deuterium, lastUpdateString);
        try
        {
            binaryFormatter.Serialize(fileStream, resourcesEntity);
        }
        catch (Exception exception)
        {
            Logger.Error("Error during serialization: " + exception);
        }
        finally
        {
            fileStream.Close();
        }
        Logger.Debug("Saved resources: " + metal + " / " + crystal + " / " + deuterium);
        Logger.Info("Resources saved to " + _savePath);
    }

    public static (int metal, int crystal, int deuterium, DateTime lastUpdate) LoadResources()
    {
        if (!File.Exists(_savePath))
        {
            Logger.Error("Could not find file at path: " + _savePath);
            return (0, 0, 0, DateTime.Now);
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(_savePath, FileMode.Open);
        ResourcesEntity resourcesEntity;
        try
        {
            resourcesEntity = (ResourcesEntity)binaryFormatter.Deserialize(fileStream);
        }
        catch (Exception exception)
        {
            Logger.Error("Error during deserialization: " + exception);
            return (0, 0, 0, DateTime.Now);
        }
        finally
        {
            fileStream.Close();
        }

        DateTime lastUpdate = DateHelper.UniversalDateFromString(resourcesEntity.LastUpdate) ?? DateTime.Now;

        return (resourcesEntity.Metal, resourcesEntity.Crystal, resourcesEntity.Deuterium, lastUpdate);
    }

}
