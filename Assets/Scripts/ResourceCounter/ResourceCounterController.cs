using System;

public interface IResourceCounterController
{
    void Start();
    void UpdateUI();
    void OnApplicationQuit();
}

public class ResourceCounterController : IResourceCounterController
{
    #region Private Fields
    private IResourceCounterView _resourcesCounterView = null;

    // The ResourceCounterController is not responsibly for building levels, but we save
    // mine levels here as well since we need them to calculate resource production.
    private int _metalMineLevel = 1;
    private int _crystalMineLevel = 1;
    private int _deuteriumMineLevel = 1;

    private (int, int, int) _deductedResourcesOnNextTick = (0, 0, 0);
    #endregion

    #region Initialiser / Finaliser
    public ResourceCounterController(IResourceCounterView resourceCounterView)
    {
        _resourcesCounterView = resourceCounterView;
    }

    ~ResourceCounterController()
    {
        DeregisterFromEvents();
    }
    #endregion

    #region Public Functions
    public void Start()
    {
        RegisterForEvents();
        LoadResources();
    }

    public void UpdateUI()
    {
        int newMetalValue = Resources.Instance.Metal + Balancing.ResourceProductionPerSecond(BuildingType.MetalMine, _metalMineLevel) - _deductedResourcesOnNextTick.Item1;
        int newCrystalValue = Resources.Instance.Crystal + Balancing.ResourceProductionPerSecond(BuildingType.CrystalMine, _crystalMineLevel) - _deductedResourcesOnNextTick.Item2;
        int newDeuteriumValue = Resources.Instance.Deuterium + Balancing.ResourceProductionPerSecond(BuildingType.DeuteriumMine, _deuteriumMineLevel) - _deductedResourcesOnNextTick.Item3;
        Resources.Instance.Metal = newMetalValue;
        Resources.Instance.Crystal = newCrystalValue;
        Resources.Instance.Deuterium = newDeuteriumValue;

        Logger.Debug("Resources deducted: " + _deductedResourcesOnNextTick);

        // TODO What if something gets changed in the meantime?
        _deductedResourcesOnNextTick = (0, 0, 0);

        if (null == _resourcesCounterView)
        {
            Logger.Error("_resourcesCounterView is null.");
            return;
        }
        _resourcesCounterView.UpdateText(Resources.Instance.Metal, Resources.Instance.Crystal, Resources.Instance.Deuterium);
    }

    public void OnApplicationQuit()
    {
        Database.SaveResources();
    }
    #endregion

    #region Private Functions
    private void LoadResources()
    {
        Database.LoadResources();

        int secondsSinceLastUpdate = DateHelper.DifferenceToNowInSeconds(Resources.Instance.LastUpdate);

        int addedMetal = secondsSinceLastUpdate * Balancing.ResourceProductionPerSecond(BuildingType.MetalMine, _metalMineLevel);
        int addedCrystal = secondsSinceLastUpdate * Balancing.ResourceProductionPerSecond(BuildingType.CrystalMine, _crystalMineLevel);
        int addedDeuterium = secondsSinceLastUpdate * Balancing.ResourceProductionPerSecond(BuildingType.DeuteriumMine, _deuteriumMineLevel);

        Resources.Instance.Metal += addedMetal;
        Resources.Instance.Crystal += addedCrystal;
        Resources.Instance.Deuterium += addedDeuterium;

        Logger.Info("Metal added: " + addedMetal);
        Logger.Info("Crystal added: " + addedCrystal);
        Logger.Info("Deuterium added: " + addedDeuterium);
    }

    private void RegisterForEvents()
    {
        if (null == EventManager.EventResourcesUsed)
        {
            Logger.Error("EventManager.ResourcesUsed is null.");
            return;
        }

        EventManager.EventResourcesUsed.AddListener(ResourcesUsed);
        EventManager.EventBuildingLevelsChanged.AddListener(SaveNewBuildingLevels);
    }

    private void DeregisterFromEvents()
    {
        if (null == EventManager.EventResourcesUsed)
        {
            Logger.Error("EventManager.ResourcesUsed is null.");
            return;
        }

        EventManager.EventResourcesUsed.RemoveListener(ResourcesUsed);
        EventManager.EventBuildingLevelsChanged.RemoveListener(SaveNewBuildingLevels);
    }

    private void ResourcesUsed(int metal, int crystal, int deuterium)
    {
        _deductedResourcesOnNextTick.Item1 += metal;
        _deductedResourcesOnNextTick.Item2 += crystal;
        _deductedResourcesOnNextTick.Item3 += deuterium;
    }

    private void SaveNewBuildingLevels(int metalMineLevel, int crystelMineLevel, int deuteriumMineLevel)
    {
        _metalMineLevel = metalMineLevel;
        _crystalMineLevel = crystelMineLevel;
        _deuteriumMineLevel = deuteriumMineLevel;
    }
    #endregion
}
