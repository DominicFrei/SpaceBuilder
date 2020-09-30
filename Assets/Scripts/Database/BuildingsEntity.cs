using System;

[Serializable]
public class BuildingsEntity
{
    public BuildingEntity MetalMine;
    public BuildingEntity CrystalMine;
    public BuildingEntity DeuteriumMine;

    public BuildingsEntity(BuildingEntity metalMine, BuildingEntity crystalMine, BuildingEntity deuteriumMine)
    {
        MetalMine = metalMine;
        CrystalMine = crystalMine;
        DeuteriumMine = deuteriumMine;
    }
}
