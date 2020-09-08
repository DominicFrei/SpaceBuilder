using System;

[Serializable]
public class BuildingsEntity
{
    public BuildingEntity MetalMine { get; private set; }
    public BuildingEntity CrystalMine { get; private set; }
    public BuildingEntity DeuteriumMine { get; private set; }

    public BuildingsEntity(BuildingEntity metalMine, BuildingEntity crystalMine, BuildingEntity deuteriumMine)
    {
        MetalMine = metalMine;
        CrystalMine = crystalMine;
        DeuteriumMine = deuteriumMine;
    }
}
