using UnityEngine;
using UnityEngine.UI;

public class BuildingList : MonoBehaviour
{
    [SerializeField] private Text _textMetalMine = null;
    [SerializeField] private Text _textCrystalMine = null;
    [SerializeField] private Text _textDeuteriumMine = null;

    private int _metalMineLevel = 1;
    private int _crystalMineLevel = 1;
    private int _deuteriumMineLevel = 1;

    private void Start()
    {
        LoadBuildingData();
        UpdateUI();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefsHelper.SaveBuildingLevel(PlayerPrefsHelper.Building.metalMine, _metalMineLevel);
        PlayerPrefsHelper.SaveBuildingLevel(PlayerPrefsHelper.Building.crystalMine, _crystalMineLevel);
        PlayerPrefsHelper.SaveBuildingLevel(PlayerPrefsHelper.Building.deuteriumMine, _deuteriumMineLevel);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "OnClick() for button")]
    private void MetalMineUpgradeClicked()
    {
        _metalMineLevel += 1;
        UpdateUI();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "OnClick() for button")]
    private void CrystalMineUpgradeClicked()
    {
        _crystalMineLevel += 1;
        UpdateUI();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "OnClick() for button")]
    private void DeuteriumMineUpgradeClicked()
    {
        _deuteriumMineLevel += 1;
        UpdateUI();
    }

    private void LoadBuildingData()
    {
        _metalMineLevel = PlayerPrefsHelper.LoadBuildingLevel(PlayerPrefsHelper.Building.metalMine);
        _crystalMineLevel = PlayerPrefsHelper.LoadBuildingLevel(PlayerPrefsHelper.Building.crystalMine);
        _deuteriumMineLevel = PlayerPrefsHelper.LoadBuildingLevel(PlayerPrefsHelper.Building.deuteriumMine);

        // Make sure the level is at least 1 (which could be 0 on the first load).
        _metalMineLevel = Mathf.Max(_metalMineLevel, 1);
        _crystalMineLevel = Mathf.Max(_crystalMineLevel, 1);
        _deuteriumMineLevel = Mathf.Max(_deuteriumMineLevel, 1);
    }

    private void UpdateUI()
    {
        _textMetalMine.text = "Metal Mine (Level " + _metalMineLevel + ")";
        _textCrystalMine.text = "Crystal Mine (Level " + _crystalMineLevel + ")";
        _textDeuteriumMine.text = "Deuterium Mine (Level " + _deuteriumMineLevel + ")";
    }
}
