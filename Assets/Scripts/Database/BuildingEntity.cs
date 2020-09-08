using System;

[Serializable]
public class BuildingEntity
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public bool IsUpgrading { get; private set; }
    public DateTime? UpgradeFinishedAt { get; private set; }

    public BuildingEntity(string name, int level, bool isUpgrading, DateTime? upgradeFinishedAt)
    {
        Name = name;
        Level = level;
        IsUpgrading = isUpgrading;
        UpgradeFinishedAt = upgradeFinishedAt;
    }
}
