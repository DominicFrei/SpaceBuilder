using System;

[Serializable]
public class GameState
{
    public ResourcesEntity ResourceEntity { get; private set; }
    public BuildingsEntity BuildingsEntity { get; private set; }

    public GameState(ResourcesEntity resourceEntity, BuildingsEntity buildingsEntity)
    {
        this.ResourceEntity = resourceEntity;
        this.BuildingsEntity = buildingsEntity;
    }
}
