using System;

public sealed class Balancing
{
    public static int ResourceProductionPerSecond(BuildingType building, int level)
    {
        int resourcesPerSecond = level;
        switch (building)
        {
            case BuildingType.MetalMine:
                resourcesPerSecond *= 20;
                break;
            case BuildingType.CrystalMine:
                resourcesPerSecond *= 10;
                break;
            case BuildingType.DeuteriumMine:
                resourcesPerSecond *= 5;
                break;
            default:
                break;
        }
        Logger.Debug(resourcesPerSecond + "");
        return resourcesPerSecond;
    }

    public static (int, int, int) ResourceCostForUpdate(BuildingEntity building)
    {
        int upgradeCostMetal = 100 * building.Level * building.Level;
        int upgradeCostCrystal = 50 * building.Level * building.Level;
        int upgradeCostDeuterium = 25 * building.Level * building.Level;

        return (upgradeCostMetal, upgradeCostCrystal, upgradeCostDeuterium);
    }

    public static DateTime UpgradeFinishedAt(BuildingEntity building)
    {
        return DateTime.UtcNow.AddSeconds(3);
    }
}
