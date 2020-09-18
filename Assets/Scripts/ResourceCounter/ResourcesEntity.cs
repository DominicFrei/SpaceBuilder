using System;

[Serializable]
public sealed class Resources
{
    // Singleton
    private static readonly Lazy<Resources> lazy = new Lazy<Resources>(() => new Resources());
    public static Resources Instance { get { return lazy.Value; } }
    private Resources() {}

    public int Metal = 0;
    public int Crystal = 0;
    public int Deuterium = 0;

    public string LastUpdate = "";
}