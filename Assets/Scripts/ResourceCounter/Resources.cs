using System;

public sealed class Resources
{
    // Singleton
    private static readonly Lazy<Resources> lazy = new Lazy<Resources>(() => new Resources());
    public static Resources Instance { get { return lazy.Value; } }
    private Resources() {}

    public int Metal;
    public int Crystal;
    public int Deuterium;

    public DateTime LastUpdate;
}