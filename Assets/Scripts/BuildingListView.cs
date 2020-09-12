﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IBuildingListView
{
    void UpdateMetalMineText(string text);
    void UpdateCrystalMineText(string text);
    void UpdateDeuteriumMineText(string text);

    void UpdateMetalMineButtonText(string text);
    void UpdateCrystalMineButtonText(string text);
    void UpdateDeuteriumMineButtonText(string text);

    void SetButtonInteractibility(bool isInteractable);
}

public class BuildingListView : MonoBehaviour, IBuildingListView
{
    [SerializeField] private Text _textMetalMine = null;
    [SerializeField] private Text _textCrystalMine = null;
    [SerializeField] private Text _textDeuteriumMine = null;

    [SerializeField] private Button _metalMineUpgradeButton = null;
    [SerializeField] private Button _crystalMineUpgradeButton = null;
    [SerializeField] private Button _deuteriumMineUpgradeButton = null;

    private IBuildingListController _buildingListController = null;

    private readonly WaitForSeconds _buldingsListUpdateInterval = new WaitForSeconds(1.0f);

    #region View Lifecycle

    private void Start()
    {
        _buildingListController = new BuildingListController(this);

        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.Start();
        _ = StartCoroutine(UpdateUICoroutine());
    }

    private IEnumerator UpdateUICoroutine()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            yield break;
        }

        while (true)
        {
            _buildingListController.UpdateUI();
            yield return _buldingsListUpdateInterval;
        }
    }

    private void OnApplicationQuit()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.OnApplicationQuit();
    }

    #endregion

    #region UI

    public void UpdateMetalMineText(string text)
    {
        if (null == _textMetalMine)
        {
            Logger.Error("_textMetalMine is null.");
            return;
        }

        _textMetalMine.text = text;
    }

    public void UpdateCrystalMineText(string text)
    {
        if (null == _textCrystalMine)
        {
            Logger.Error("_textCrystalMine is null.");
            return;
        }

        _textCrystalMine.text = text;
    }

    public void UpdateDeuteriumMineText(string text)
    {
        if (null == _textDeuteriumMine)
        {
            Logger.Error("_textDeuteriumMine is null.");
            return;
        }

        _textDeuteriumMine.text = text;
    }

    public void UpdateMetalMineButtonText(string text)
    {
        if (null == _metalMineUpgradeButton)
        {
            Logger.Error("_metalMineUpgradeButton is null.");
            return;
        }

        Text buttonText = _metalMineUpgradeButton.GetComponentInChildren<Text>();
        if (null == buttonText)
        {
            Logger.Error("_metalMineUpgradeButton does not have a text component.");
            return;
        }

        buttonText.text = text;
    }

    public void UpdateCrystalMineButtonText(string text)
    {
        if (null == _crystalMineUpgradeButton)
        {
            Logger.Error("_crystalMineUpgradeButton is null.");
            return;
        }

        Text buttonText = _crystalMineUpgradeButton.GetComponentInChildren<Text>();
        if (null == buttonText)
        {
            Logger.Error("_crystalMineUpgradeButton does not have a text component.");
            return;
        }

        buttonText.text = text;
    }

    public void UpdateDeuteriumMineButtonText(string text)
    {
        if (null == _deuteriumMineUpgradeButton)
        {
            Logger.Error("_deuteriumMineUpgradeButton is null.");
            return;
        }

        Text buttonText = _deuteriumMineUpgradeButton.GetComponentInChildren<Text>();
        if (null == buttonText)
        {
            Logger.Error("_deuteriumMineUpgradeButton does not have a text component.");
            return;
        }

        buttonText.text = text;
    }

    public void SetButtonInteractibility(bool isInteractable)
    {
        if (null == _metalMineUpgradeButton || null == _crystalMineUpgradeButton || null == _deuteriumMineUpgradeButton)
        {
            Logger.Error("At least on button is null.");
            return;
        }

        _metalMineUpgradeButton.interactable = isInteractable;
        _crystalMineUpgradeButton.interactable = isInteractable;
        _deuteriumMineUpgradeButton.interactable = isInteractable;
    }

    #endregion

    #region User Interaction

    public void MetalMineUpgradeClicked()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.MetalMineUpgradeClicked();
    }

    public void CrystalMineUpgradeClicked()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.CrystalMineUpgradeClicked();
    }

    public void DeuteriumMineUpgradeClicked()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.DeuteriumMineUpgradeClicked();
    }

    #endregion

}
