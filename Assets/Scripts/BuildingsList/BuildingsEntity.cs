using System;

[Serializable]
public class BuildingsEntity
{
    public BuildingEntity MetalMine;
    public BuildingEntity CrystalMine;
    public BuildingEntity DeuteriumMine;
    public BuildingEntity Shipyard;

    public BuildingsEntity(BuildingEntity metalMine, BuildingEntity crystalMine, BuildingEntity deuteriumMine, BuildingEntity shipyard)
    {
        MetalMine = metalMine;
        CrystalMine = crystalMine;
        DeuteriumMine = deuteriumMine;
        Shipyard = shipyard;
    }
}
