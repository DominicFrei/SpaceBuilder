using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildingList : MonoBehaviour
{
    [SerializeField] private Text _textMetalMine = null;
    [SerializeField] private Text _textCrystalMine = null;
    [SerializeField] private Text _textDeuteriumMine = null;
    [SerializeField] private Button _metalMineUpgradeButton = null;
    [SerializeField] private Button _crystalMineUpgradeButton = null;
    [SerializeField] private Button _deuteriumMineUpgradeButton = null;

    private BuildingEntity _metalMine = null;
    private BuildingEntity _crystalMine = null;
    private BuildingEntity _deuteriumMine = null;

    private readonly WaitForSeconds _buldingsListUpdateInterval = new WaitForSeconds(1.0f);

    private void Start()
    {
        LoadBuildingData();
        _ = StartCoroutine(UpdateUICoroutine());
    }

    private void OnApplicationQuit()
    {
        BuildingsEntity buildingsEntity = new BuildingsEntity(_metalMine, _crystalMine, _deuteriumMine);
        Database.SaveBuildings(buildingsEntity);
    }

    public void MetalMineUpgradeClicked()
    {
        _metalMine.IsUpgrading = true;
        _metalMine.UpgradeFinishedAt = DateTime.UtcNow.AddSeconds(10);
        UpdateUI();
    }

    public void CrystalMineUpgradeClicked()
    {
        _crystalMine.IsUpgrading = true;
        _crystalMine.UpgradeFinishedAt = DateTime.UtcNow.AddSeconds(10);
        UpdateUI();
    }

    public void DeuteriumMineUpgradeClicked()
    {
        _deuteriumMine.IsUpgrading = true;
        _deuteriumMine.UpgradeFinishedAt = DateTime.UtcNow.AddSeconds(10);
        UpdateUI();
    }

    private void LoadBuildingData()
    {
        BuildingsEntity buildingsEntity = Database.LoadBuildings();
        _metalMine = buildingsEntity.MetalMine;
        _crystalMine = buildingsEntity.CrystalMine;
        _deuteriumMine = buildingsEntity.DeuteriumMine;
    }

    private IEnumerator UpdateUICoroutine()
    {
        while (true)
        {
            UpdateUI();
            yield return _buldingsListUpdateInterval;
        }
    }

    private void UpdateUI()
    {
        CheckBuildingUpgrade(_metalMine, _textMetalMine, _metalMineUpgradeButton);
        CheckBuildingUpgrade(_crystalMine, _textCrystalMine, _crystalMineUpgradeButton);
        CheckBuildingUpgrade(_deuteriumMine, _textDeuteriumMine, _deuteriumMineUpgradeButton);
    }

    private void CheckBuildingUpgrade(BuildingEntity building, Text text, Button button)
    {
        if (building.IsUpgrading)
        {
            if (null != building.UpgradeFinishedAt && DateTime.UtcNow < building.UpgradeFinishedAt)
            {
                SetButtonInteractibility(false);
                TimeSpan timeTillUpdate = (building.UpgradeFinishedAt ?? DateTime.UtcNow) - DateTime.UtcNow;
                int timeInSeconds = (int)timeTillUpdate.TotalSeconds;
                button.GetComponentInChildren<Text>().text = "Upgrading ... (" + timeInSeconds + " seconds)";

                Logger.Debug("Upgrade for " + building.Name + " has started.");
            }
            else if (null != building.UpgradeFinishedAt && DateTime.UtcNow >= building.UpgradeFinishedAt)
            {
                building.Level += 1;
                building.IsUpgrading = false;
                building.UpgradeFinishedAt = null;

                int upgradeCostMetal = building.Level * 100;
                int upgradeCostCrystal = building.Level * 50;
                int upgradeCostDeuterium = building.Level * 25;

                text.text = building.Name + "(Level " + building.Level + ")\n"
                            + "  Next Upgrade:\n"
                            + "  " + upgradeCostMetal + " Metal / " + upgradeCostCrystal + " Crystal / " + upgradeCostDeuterium + " Deuterium";
                button.GetComponentInChildren<Text>().text = "Upgrade";
                SetButtonInteractibility(true);

                Logger.Debug("Upgrade for " + building.Name + " has finished.");
            }
            else
            {
                Logger.Warning("This branch should not be called ever. If a building is upgrading, a finish date has to be set.");
            }
        }
        else
        {
            int upgradeCostMetal = building.Level * 100;
            int upgradeCostCrystal = building.Level * 50;
            int upgradeCostDeuterium = building.Level * 25;

            text.text = building.Name + "(Level " + building.Level + ")\n"
                            + "  Next Upgrade:\n"
                            + "  " + upgradeCostMetal + " Metal / " + upgradeCostCrystal + " Crystal / " + upgradeCostDeuterium + " Deuterium";
            button.GetComponentInChildren<Text>().text = "Upgrade";
            Logger.Debug("No upgrade running for " + building.Name + ".");
        }
    }

    private void SetButtonInteractibility(bool active)
    {
        _metalMineUpgradeButton.interactable = active;
        _crystalMineUpgradeButton.interactable = active;
        _deuteriumMineUpgradeButton.interactable = active;
    }

}
