using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IBuildingListView
{
    // Text Objects
    void UpdateMetalMineText(string text);
    void UpdateCrystalMineText(string text);
    void UpdateDeuteriumMineText(string text);
    void UpdateShipyardText(string text);

    // Button Objects Texts
    void UpdateMetalMineButtonText(string text);
    void UpdateCrystalMineButtonText(string text);
    void UpdateDeuteriumMineButtonText(string text);
    void UpdateShipyardButtonText(string text);

    // Button Objects Activation / Deactivation
    void SetMetalMineButtonInteractable(bool isInteractable);
    void SetCrystallMineButtonInteractable(bool isInteractable);
    void SetDeuteriumMineButtonInteractable(bool isInteractable);
    void SetShipyardButtonInteractable(bool isInteractable);
}

public class BuildingListView : MonoBehaviour, IBuildingListView
{
    #region Private Fields
    [SerializeField] private Text _textMetalMine = null;
    [SerializeField] private Text _textCrystalMine = null;
    [SerializeField] private Text _textDeuteriumMine = null;
    [SerializeField] private Text _textShipyard = null;

    [SerializeField] private Button _buttonUpgradeMatelMine = null;
    [SerializeField] private Button _buttonUpgradeCrystalMine = null;
    [SerializeField] private Button _buttonUpgradeDeuteriumMine = null;
    [SerializeField] private Button _buttonUpgradeShipyard = null;

    private IBuildingListController _buildingListController = null;

    private readonly WaitForSeconds _buldingsListUpdateInterval = new WaitForSeconds(1.0f);
    #endregion

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

    #region Public Functions
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

    public void UpdateShipyardText(string text)
    {
        if (null == _textShipyard)
        {
            Logger.Error("_textShipyard is null.");
            return;
        }

        _textShipyard.text = text;
    }

    public void UpdateMetalMineButtonText(string text)
    {
        if (null == _buttonUpgradeMatelMine)
        {
            Logger.Error("_metalMineUpgradeButton is null.");
            return;
        }

        Text buttonText = _buttonUpgradeMatelMine.GetComponentInChildren<Text>();
        if (null == buttonText)
        {
            Logger.Error("_metalMineUpgradeButton does not have a text component.");
            return;
        }

        buttonText.text = text;
    }

    public void UpdateCrystalMineButtonText(string text)
    {
        if (null == _buttonUpgradeCrystalMine)
        {
            Logger.Error("_crystalMineUpgradeButton is null.");
            return;
        }

        Text buttonText = _buttonUpgradeCrystalMine.GetComponentInChildren<Text>();
        if (null == buttonText)
        {
            Logger.Error("_crystalMineUpgradeButton does not have a text component.");
            return;
        }

        buttonText.text = text;
    }

    public void UpdateDeuteriumMineButtonText(string text)
    {
        if (null == _buttonUpgradeDeuteriumMine)
        {
            Logger.Error("_deuteriumMineUpgradeButton is null.");
            return;
        }

        Text buttonText = _buttonUpgradeDeuteriumMine.GetComponentInChildren<Text>();
        if (null == buttonText)
        {
            Logger.Error("_deuteriumMineUpgradeButton does not have a text component.");
            return;
        }

        buttonText.text = text;
    }

    public void UpdateShipyardButtonText(string text)
    {
        if (null == _buttonUpgradeShipyard)
        {
            Logger.Error("_buttonUpgradeShipyard is null.");
            return;
        }

        Text buttonText = _buttonUpgradeShipyard.GetComponentInChildren<Text>();
        if (null == buttonText)
        {
            Logger.Error("_buttonUpgradeShipyard does not have a text component.");
            return;
        }

        buttonText.text = text;
    }

    public void SetMetalMineButtonInteractable(bool isInteractable)
    {
        if (null == _buttonUpgradeMatelMine)
        {
            Logger.Error("_metalMineUpgradeButton is null.");
            return;
        }

        _buttonUpgradeMatelMine.interactable = isInteractable;
    }

    public void SetCrystallMineButtonInteractable(bool isInteractable)
    {
        if (null == _buttonUpgradeCrystalMine)
        {
            Logger.Error("_crystalMineUpgradeButton is null.");
            return;
        }

        _buttonUpgradeCrystalMine.interactable = isInteractable;
    }

    public void SetDeuteriumMineButtonInteractable(bool isInteractable)
    {
        if (null == _buttonUpgradeDeuteriumMine)
        {
            Logger.Error("_deuteriumMineUpgradeButton is null.");
            return;
        }

        _buttonUpgradeDeuteriumMine.interactable = isInteractable;
    }

    public void SetShipyardButtonInteractable(bool isInteractable)
    {
        if (null == _buttonUpgradeShipyard)
        {
            Logger.Error("_buttonUpgradeShipyard is null.");
            return;
        }

        _buttonUpgradeShipyard.interactable = isInteractable;
    }
    #endregion

    #region User Interaction
    public void UpgradeMetalMineClicked()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.UpgradeMetalMineClicked();
    }

    public void UpgradeCrystalMineClicked()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.UpgradeCrystalMineClicked();
    }

    public void UpgradeDeuteriumMineClicked()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.UpgradeDeuteriumMineClicked();
    }

    public void UpgradeShipyardClicked()
    {
        if (null == _buildingListController)
        {
            Logger.Error("_buildingListController is null.");
            return;
        }

        _buildingListController.UpgradeShipyardClicked();
    }
    #endregion

}
