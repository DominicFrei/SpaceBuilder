using System;

public interface IBuildingListController
{
    void Start();
    void UpdateUI();
    void OnApplicationQuit();

    void MetalMineUpgradeClicked();
    void CrystalMineUpgradeClicked();
    void DeuteriumMineUpgradeClicked();
}

public class BuildingListController : IBuildingListController
{
    private IBuildingListView _buildingListView;

    private BuildingEntity _metalMine = null;
    private BuildingEntity _crystalMine = null;
    private BuildingEntity _deuteriumMine = null;

    private enum BuildingType
    {
        MetalMine,
        CrystalMine,
        DeuteriumMine
    }

    public BuildingListController(IBuildingListView buildingListView)
    {
        _buildingListView = buildingListView;
    }

    public void Start()
    {
        BuildingsEntity buildingsEntity = Database.LoadBuildings();
        _metalMine = buildingsEntity.MetalMine;
        _crystalMine = buildingsEntity.CrystalMine;
        _deuteriumMine = buildingsEntity.DeuteriumMine;
    }

    public void OnApplicationQuit()
    {
        BuildingsEntity buildingsEntity = new BuildingsEntity(_metalMine, _crystalMine, _deuteriumMine);
        Database.SaveBuildings(buildingsEntity);
    }

    public void MetalMineUpgradeClicked()
    {
        if (null == _metalMine)
        {
            Logger.Error("_metalMine is null.");
            return;
        }

        _metalMine.IsUpgrading = true;
        _metalMine.UpgradeFinishedAt = DateTime.UtcNow.AddSeconds(10);
        UpdateUI();
    }

    public void CrystalMineUpgradeClicked()
    {
        if (null == _crystalMine)
        {
            Logger.Error("_crystalMine is null.");
            return;
        }

        _crystalMine.IsUpgrading = true;
        _crystalMine.UpgradeFinishedAt = DateTime.UtcNow.AddSeconds(10);
        UpdateUI();
    }

    public void DeuteriumMineUpgradeClicked()
    {
        if (null == _deuteriumMine)
        {
            Logger.Error("_deuteriumMine is null.");
            return;
        }

        _deuteriumMine.IsUpgrading = true;
        _deuteriumMine.UpgradeFinishedAt = DateTime.UtcNow.AddSeconds(10);

        UpdateUI();
    }

    public void UpdateUI()
    {
        bool isMetalMineUpgrading = CheckBuildingUpgrade(BuildingType.MetalMine);
        bool isCrystalMineUpgrading = CheckBuildingUpgrade(BuildingType.CrystalMine);
        bool isDeuteriumMineUpgrading = CheckBuildingUpgrade(BuildingType.DeuteriumMine);

        bool isAtLeastOneMineUpgrading = isMetalMineUpgrading || isCrystalMineUpgrading || isDeuteriumMineUpgrading;
        _buildingListView.SetButtonInteractibility(!isAtLeastOneMineUpgrading);
    }

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

        int upgradeCostMetal = building.Level * 100;
        int upgradeCostCrystal = building.Level * 50;
        int upgradeCostDeuterium = building.Level * 25;

        if (building.IsUpgrading)
        {
            if (null != building.UpgradeFinishedAt && DateTime.UtcNow < building.UpgradeFinishedAt)
            {
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

                buildingText = building.Name + "(Level " + building.Level + ")\n"
                            + "  Next Upgrade:\n"
                            + "  " + upgradeCostMetal + " Metal / " + upgradeCostCrystal + " Crystal / " + upgradeCostDeuterium + " Deuterium";
                buttonText = "Upgrade";

                Logger.Debug("Upgrade for " + building.Name + " has finished.");
            }
            else
            {
                Logger.Warning("This branch should not be called ever. If a building is upgrading, a finish date has to be set.");
            }
        }
        else
        {
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

}
