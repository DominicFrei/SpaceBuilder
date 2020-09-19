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
        bool isMetalMineUpgrading = CheckBuildingUpgrade(BuildingType.MetalMine);
        bool isCrystalMineUpgrading = CheckBuildingUpgrade(BuildingType.CrystalMine);
        bool isDeuteriumMineUpgrading = CheckBuildingUpgrade(BuildingType.DeuteriumMine);

        bool isAtLeastOneMineUpgrading = isMetalMineUpgrading || isCrystalMineUpgrading || isDeuteriumMineUpgrading;
        _buildingListView.SetButtonInteractibility(!isAtLeastOneMineUpgrading);
    }

    public void OnApplicationQuit()
    {
        BuildingsEntity buildingsEntity = new BuildingsEntity(_metalMine, _crystalMine, _deuteriumMine);
        Database.SaveBuildings(buildingsEntity);
    }

    public void MetalMineUpgradeClicked()
    {
        ApplyUpgradeForBuilding(_metalMine);
    }

    public void CrystalMineUpgradeClicked()
    {
        ApplyUpgradeForBuilding(_crystalMine);
    }

    public void DeuteriumMineUpgradeClicked()
    {
        ApplyUpgradeForBuilding(_deuteriumMine);
    }
    #endregion

    #region Private Functions
    // TODO: rework for readability
    private bool CheckBuildingUpgrade(BuildingType buildingType)
    {
        BuildingEntity building;
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
                Logger.Error("Invalid building type.");
                return false;
        }

        string buildingText = "";
        string buttonText = "";

        if (building.IsUpgrading)
        {
            if (null != building.UpgradeFinishedAt && DateTime.UtcNow < building.UpgradeFinishedAt)
            {
                (int upgradeCostMetal, int upgradeCostCrystal, int upgradeCostDeuterium) = Balancing.ResourceCostForUpdate(building);
                buildingText = building.Name + "(Level " + building.Level + ")\n"
                            + "  Next Upgrade:\n"
                            + "  " + upgradeCostMetal + " Metal / " + upgradeCostCrystal + " Crystal / " + upgradeCostDeuterium + " Deuterium";
                TimeSpan timeTillUpdate = (building.UpgradeFinishedAt ?? DateTime.UtcNow) - DateTime.UtcNow;
                int timeInSeconds = (int)timeTillUpdate.TotalSeconds;
                buttonText = "Upgrading ... (" + timeInSeconds + " seconds)";

                Logger.Debug("Upgrade for " + building.Name + " has started.");
            }
            else if (null != building.UpgradeFinishedAt && DateTime.UtcNow >= building.UpgradeFinishedAt)
            {
                building.Level += 1;
                building.IsUpgrading = false;
                building.UpgradeFinishedAt = null;

                (int upgradeCostMetal, int upgradeCostCrystal, int upgradeCostDeuterium) = Balancing.ResourceCostForUpdate(building);
                buildingText = building.Name + "(Level " + building.Level + ")\n"
                            + "  Next Upgrade:\n"
                            + "  " + upgradeCostMetal + " Metal / " + upgradeCostCrystal + " Crystal / " + upgradeCostDeuterium + " Deuterium";
                buttonText = "Upgrade";

                if (null == EventManager.EventBuildingLevelsChanged)
                {
                    Logger.Error("EventManager.BuildingLevelsChanged is null.");
                }
                else
                {
                    EventManager.EventBuildingLevelsChanged.Invoke(_metalMine.Level, _crystalMine.Level, _deuteriumMine.Level);
                }

                Logger.Debug("Upgrade for " + building.Name + " has finished.");
            }
            else
            {
                Logger.Warning("This branch should not be called ever. If a building is upgrading, a finish date has to be set.");
            }
        }
        else
        {
            (int upgradeCostMetal, int upgradeCostCrystal, int upgradeCostDeuterium) = Balancing.ResourceCostForUpdate(building);
            buildingText = building.Name + "(Level " + building.Level + ")\n"
                            + "  Next Upgrade:\n"
                            + "  " + upgradeCostMetal + " Metal / " + upgradeCostCrystal + " Crystal / " + upgradeCostDeuterium + " Deuterium";
            buttonText = "Upgrade";

            Logger.Debug("No upgrade running for " + building.Name + ".");
        }

        if (buildingText.Equals(String.Empty))
        {
            Logger.Error("buildingText was not set correctly.");
        }
        if (buttonText.Equals(String.Empty))
        {
            Logger.Error("buttonText was not set correctly.");
        }

        switch (buildingType)
        {
            case BuildingType.MetalMine:
                _buildingListView.UpdateMetalMineText(buildingText);
                _buildingListView.UpdateMetalMineButtonText(buttonText);
                break;
            case BuildingType.CrystalMine:
                _buildingListView.UpdateCrystalMineText(buildingText);
                _buildingListView.UpdateCrystalMineButtonText(buttonText);
                break;
            case BuildingType.DeuteriumMine:
                _buildingListView.UpdateDeuteriumMineText(buildingText);
                _buildingListView.UpdateDeuteriumMineButtonText(buttonText);
                break;
            default:
                Logger.Error("Invalid building type.");
                return false;
        }

        return building.IsUpgrading;
    }

    private void ApplyUpgradeForBuilding(BuildingEntity building)
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

            UpdateUI();
        }
        else
        {
            Logger.Info("Building cannot be built, not enough resources.");
        }
    }
    #endregion

}
