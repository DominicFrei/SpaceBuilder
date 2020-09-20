using System;

[Serializable]
public class BuildingListEntity
{
    public BuildingEntity MetalMine;
    public BuildingEntity CrystalMine;
    public BuildingEntity DeuteriumMine;
    public BuildingEntity Shipyard;

    public BuildingListEntity(BuildingEntity metalMine, BuildingEntity crystalMine, BuildingEntity deuteriumMine, BuildingEntity shipyard)
    {
        MetalMine = metalMine;
        CrystalMine = crystalMine;
        DeuteriumMine = deuteriumMine;
        Shipyard = shipyard;
    }
}
