using System;

public enum BuildingType
{
    MetalMine,
    CrystalMine,
    DeuteriumMine
}

[Serializable]
public class BuildingEntity
{
    public string Name;
    public BuildingType Type;
    public int Level;
    public bool IsUpgrading;
    public DateTime? UpgradeFinishedAt;

    public BuildingEntity(string name, BuildingType type, int level, bool isUpgrading, DateTime? upgradeFinishedAt)
    {
        Name = name;
        Type = type;
        Level = level;
        IsUpgrading = isUpgrading;
        UpgradeFinishedAt = upgradeFinishedAt;
    }
}
