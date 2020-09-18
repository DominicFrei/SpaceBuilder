[System.Serializable]
public class ResourcesEntity
{
    public int Metal { get; private set; }
    public int Crystal { get; private set; }
    public int Deuterium { get; private set; }
    public string LastUpdate { get; private set; }

    public ResourcesEntity(int metal, int crystal, int deuterium, string lastUpdate)
    {
        Metal = metal;
        Crystal = crystal;
        Deuterium = deuterium;
        LastUpdate = lastUpdate;
    }
}
