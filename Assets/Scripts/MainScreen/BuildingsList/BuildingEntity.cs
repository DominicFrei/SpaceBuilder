using System;

public enum BuildingType
{
    Unkown,
    MetalMine,
    CrystalMine,
    DeuteriumMine,
    Shipyard
}

[Serializable]
public class BuildingEntity
{
    public string Name = "n/a";
    public BuildingType Type = BuildingType.Unkown;
    public int Level = -1;
    public bool IsUpgrading = false;
    public DateTime? UpgradeFinishedAt = null;

    public BuildingEntity(string name, BuildingType type, int level, bool isUpgrading, DateTime? upgradeFinishedAt)
    {
        Name = name;
        Type = type;
        Level = level;
        IsUpgrading = isUpgrading;
        UpgradeFinishedAt = upgradeFinishedAt;
    }
}
