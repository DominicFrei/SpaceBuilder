using System;

[Serializable]
public class GameState
{
    public ResourcesEntity ResourceEntity;
    public BuildingList BuildingList;

    public GameState(ResourcesEntity resourceEntity, BuildingList buildingList)
    {
        this.ResourceEntity = resourceEntity;
        this.BuildingList = buildingList;
    }
}
