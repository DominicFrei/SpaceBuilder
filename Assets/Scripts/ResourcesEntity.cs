[System.Serializable]
public class ResourcesEntity
{
    public int Metal { get; private set; }
    public int Crystal { get; private set; }
    public int Deuterium { get; private set; }

    public ResourcesEntity(int metal, int crystal, int deuterium)
    {
        this.Metal = metal;
        this.Crystal = crystal;
        this.Deuterium = deuterium;
    }
}
