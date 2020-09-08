using System;

[Serializable]
public class SaveGame
{
    public ResourcesEntity ResourceEntity { get; private set; }
    public BuildingsEntity BuildingsEntity { get; private set; }

    public SaveGame(ResourcesEntity resourceEntity, BuildingsEntity buildingsEntity)
    {
        this.ResourceEntity = resourceEntity;
        this.BuildingsEntity = buildingsEntity;
    }
}
