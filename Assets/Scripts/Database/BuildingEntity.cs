using System;

[Serializable]
public class BuildingEntity
{
    public string Name;
    public int Level;
    public bool IsUpgrading;
    public DateTime? UpgradeFinishedAt;

    public BuildingEntity(string name, int level, bool isUpgrading, DateTime? upgradeFinishedAt)
    {
        Name = name;
        Level = level;
        IsUpgrading = isUpgrading;
        UpgradeFinishedAt = upgradeFinishedAt;
    }
}
