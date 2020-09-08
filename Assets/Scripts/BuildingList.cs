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
        StartCoroutine(UpdateUICoroutine());
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
        _crystalMine.Level += 1;
        UpdateUI();
    }

    public void DeuteriumMineUpgradeClicked()
    {
        _deuteriumMine.Level += 1;
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
        if (_metalMine.IsUpgrading)
        {
            if (null != _metalMine.UpgradeFinishedAt && DateTime.UtcNow < _metalMine.UpgradeFinishedAt)
            {
                _metalMineUpgradeButton.interactable = false;
                TimeSpan timeTillUpdate = (_metalMine.UpgradeFinishedAt ?? DateTime.UtcNow) - DateTime.UtcNow;
                int timeInSeconds = (int)timeTillUpdate.TotalSeconds;
                _metalMineUpgradeButton.GetComponentInChildren<Text>().text = "Upgrading ... (" + timeInSeconds + " seconds)";
                Logger.Info("Metal mine upgrade started.");
            }            
            else if (null != _metalMine.UpgradeFinishedAt && DateTime.UtcNow >= _metalMine.UpgradeFinishedAt)
            {
                _metalMine.Level += 1;
                _metalMine.IsUpgrading = false;
                _metalMine.UpgradeFinishedAt = null;

                _metalMineUpgradeButton.interactable = true;
                _metalMineUpgradeButton.GetComponentInChildren<Text>().text = "Upgrade";
                Logger.Info("Metal mine upgrade finished.");
            }
            else
            {
                Logger.Warning("Uhm ...");
            }
        }
        else
        {
            _metalMineUpgradeButton.interactable = true;
            _metalMineUpgradeButton.GetComponentInChildren<Text>().text = "Upgrade";
            Logger.Debug("No Metal mine upgrade running.");
        }
        _textMetalMine.text = "Metal Mine (Level " + _metalMine.Level + ")";
        _textCrystalMine.text = "Crystal Mine (Level " + _crystalMine.Level + ")";
        _textDeuteriumMine.text = "Deuterium Mine (Level " + _deuteriumMine.Level + ")";
    }

}
