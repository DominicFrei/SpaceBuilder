using System;

[Serializable]
public class ResourcesEntity
{
    public int Metal;
    public int Crystal;
    public int Deuterium;
    public string LastUpdate;

    public ResourcesEntity(int metal, int crystal, int deuterium, string lastUpdate)
    {
        Metal = metal;
        Crystal = crystal;
        Deuterium = deuterium;
        LastUpdate = lastUpdate;
    }
}
