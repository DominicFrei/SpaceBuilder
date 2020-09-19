using System;

public interface IBuildingListController
{
    // View Lifecycle
    void Start();
    void UpdateUI();
    void OnApplicationQuit();

    // User Input
    void MetalMineUpgradeClicked();
    void CrystalMineUpgradeClicked();
    void DeuteriumMineUpgradeClicked();
}

public class BuildingListController : IBuildingListController
{
    #region Private Fields
    private IBuildingListView _buildingListView;

    private BuildingEntity _metalMine = null;
    private BuildingEntity _crystalMine = null;
    private BuildingEntity _deuteriumMine = null;
    #endregion

    #region Initialiser / Finaliser
    public BuildingListController(IBuildingListView buildingListView)
    {
        _buildingListView = buildingListView;
    }
    #endregion

    #region Public Functions
    public void Start()
    {
        BuildingsEntity buildingsEntity = Database.LoadBuildings();
        _metalMine = buildingsEntity.MetalMine;
        _crystalMine = buildingsEntity.CrystalMine;
        _deuteriumMine = buildingsEntity.DeuteriumMine;
    }

    public void UpdateUI()
    {
        // Check if any upgrade are done
        CheckUpgradeStatusForBuilding(_metalMine);
        CheckUpgradeStatusForBuilding(_crystalMine);
        CheckUpgradeStatusForBuilding(_deuteriumMine);

        // Update building levels + upgrade costs
        UpdateTextsForBuilding(BuildingType.MetalMine);
        UpdateTextsForBuilding(BuildingType.CrystalMine);
        UpdateTextsForBuilding(BuildingType.DeuteriumMine);

        // Update button texts + interactibility
        UpdateButtonForBuilding(BuildingType.MetalMine);
        UpdateButtonForBuilding(BuildingType.CrystalMine);
        UpdateButtonForBuilding(BuildingType.DeuteriumMine);

        // Deactivate all buttons if at least one upgrade is running.
        BuildingEntity[] buildings = { _metalMine, _crystalMine, _deuteriumMine };
        bool isAtLeastOneMineUpgrading = false;
        foreach (BuildingEntity building in buildings)
        {
            if (building.IsUpgrading)
            {
                isAtLeastOneMineUpgrading = true;
                break;
            }
        }
        if (isAtLeastOneMineUpgrading)
        {
            _buildingListView.SetMetalMineButtonInteractable(false);
            _buildingListView.SetCrystallMineButtonInteractable(false);
            _buildingListView.SetDeuteriumMineButtonInteractable(false);
        }
    }

    public void OnApplicationQuit()
    {
        BuildingsEntity buildingsEntity = new BuildingsEntity(_metalMine, _crystalMine, _deuteriumMine);
        Database.SaveBuildings(buildingsEntity);
    }

    public void MetalMineUpgradeClicked()
    {
        StartUpgradeFor(_metalMine);
    }

    public void CrystalMineUpgradeClicked()
    {
        StartUpgradeFor(_crystalMine);
    }

    public void DeuteriumMineUpgradeClicked()
    {
        StartUpgradeFor(_deuteriumMine);
    }
    #endregion

    #region Private Functions
    private void CheckUpgradeStatusForBuilding(BuildingEntity building)
    {
        if (!building.IsUpgrading)
        {
            Logger.Debug("Building " + building.Name + " is not upgrading.");
            return;
        }

        if (null != building.UpgradeFinishedAt && DateTime.UtcNow >= building.UpgradeFinishedAt)
        {
            building.Level += 1;
            building.IsUpgrading = false;
            building.UpgradeFinishedAt = null;
            Logger.Info("Upgrade for " + building.Name + " has finished.");

            if (null == EventManager.EventBuildingLevelsChanged)
            {
                Logger.Warning("EventManager.BuildingLevelsChanged is null.");
                return;
            }

            EventManager.EventBuildingLevelsChanged.Invoke(_metalMine.Level, _crystalMine.Level, _deuteriumMine.Level);
        }
    }

    private void UpdateTextsForBuilding(BuildingType buildingType)
    {
        BuildingEntity building = BuildingForBuildingType(buildingType);
        (int upgradeCostMetal, int upgradeCostCrystal, int upgradeCostDeuterium) = Balancing.ResourceCostForUpdate(building);
        string buildingText = building.Name + "(Level " + building.Level + ")\n"
                        + "Next Upgrade:\n"
                        + upgradeCostMetal + " Metal / " + upgradeCostCrystal + " Crystal / " + upgradeCostDeuterium + " Deuterium";
        switch (buildingType)
        {
            case BuildingType.MetalMine:
                _buildingListView.UpdateMetalMineText(buildingText);
                break;
            case BuildingType.CrystalMine:
                _buildingListView.UpdateCrystalMineText(buildingText);
                break;
            case BuildingType.DeuteriumMine:
                _buildingListView.UpdateDeuteriumMineText(buildingText);
                break;
        }
    }

    private BuildingEntity BuildingForBuildingType(BuildingType buildingType)
    {
        BuildingEntity building = null;
        switch (buildingType)
        {
            case BuildingType.MetalMine:
                building = _metalMine;
                break;
            case BuildingType.CrystalMine:
                building = _crystalMine;
                break;
            case BuildingType.DeuteriumMine:
                building = _deuteriumMine;
                break;
            default:
                Logger.Error("Could not retrieve building from building type.");
                break;
        }
        return building;
    }

    private void StartUpgradeFor(BuildingEntity building)
    {
        if (null == building)
        {
            Logger.Error("building is null.");
            return;
        }

        (int upgradeCostMetal, int upgradeCostCrystal, int upgradeCostDeuterium) = Balancing.ResourceCostForUpdate(building);

        if (Resources.Instance.Metal >= upgradeCostMetal && Resources.Instance.Crystal >= upgradeCostCrystal && Resources.Instance.Deuterium >= upgradeCostDeuterium)
        {
            EventManager.EventResourcesUsed.Invoke(upgradeCostMetal, upgradeCostCrystal, upgradeCostDeuterium);
            building.IsUpgrading = true;
            building.UpgradeFinishedAt = Balancing.UpgradeFinishedAt(building);

            Logger.Info("Upgrade for " + building.Name + " has started.");

            UpdateUI();
        }
        else
        {
            Logger.Info("Building cannot be built, not enough resources.");
        }
    }

    private void UpdateButtonForBuilding(BuildingType buildingType)
    {
        BuildingEntity building = BuildingForBuildingType(buildingType);
        string buttonText = "Upgrade";

        if (building.IsUpgrading && null != building.UpgradeFinishedAt && DateTime.UtcNow < building.UpgradeFinishedAt)
        {
            TimeSpan timeTillUpdate = (building.UpgradeFinishedAt ?? DateTime.UtcNow) - DateTime.UtcNow;
            int timeInSeconds = (int)timeTillUpdate.TotalSeconds;
            buttonText = "Upgrading ... (" + timeInSeconds + " seconds)";
        }

        switch (buildingType)
        {
            case BuildingType.MetalMine:
                _buildingListView.SetMetalMineButtonInteractable(!building.IsUpgrading);
                _buildingListView.UpdateMetalMineButtonText(buttonText);
                break;
            case BuildingType.CrystalMine:
                _buildingListView.SetCrystallMineButtonInteractable(!building.IsUpgrading);
                _buildingListView.UpdateCrystalMineButtonText(buttonText);
                break;
            case BuildingType.DeuteriumMine:
                _buildingListView.SetDeuteriumMineButtonInteractable(!building.IsUpgrading);
                _buildingListView.UpdateDeuteriumMineButtonText(buttonText);
                break;
            default:
                Logger.Warning("Invalid building type.");
                break;
        }
    }
    #endregion

}
