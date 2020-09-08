using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{
    private static readonly string _savePath = Application.persistentDataPath + "/resources.bin";

    public static void SaveResources(int metal, int crystal, int deuterium)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(_savePath, FileMode.Create);
        ResourcesEntity resourcesEntity = new ResourcesEntity(metal, crystal, deuterium);
        try
        {
            binaryFormatter.Serialize(fileStream, resourcesEntity);
        }
        catch (Exception exception)
        {
            Debug.LogError("Error during serialization: " + exception);
        }
        finally
        {
            fileStream.Close();
        }        
        Debug.Log("Data saved to " + _savePath);
    }

    public static (int metal, int crystal, int deuterium) LoadResources()
    {
        if (!File.Exists(_savePath))
        {
            Debug.LogError("Could not find file at path: " + _savePath);
            return (0, 0, 0);
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
            Debug.LogError("Error during deserialization: " + exception);
            return (0, 0, 0);
        }
        finally
        {
            fileStream.Close();
        }

        return (resourcesEntity.Metal, resourcesEntity.Crystal, resourcesEntity.Deuterium);
    }

}
