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
        Database.SaveBuildingLevel(Database.Building.metalMine, _metalMineLevel);
        Database.SaveBuildingLevel(Database.Building.crystalMine, _crystalMineLevel);
        Database.SaveBuildingLevel(Database.Building.deuteriumMine, _deuteriumMineLevel);
    }

    public void MetalMineUpgradeClicked()
    {
        _metalMineLevel += 1;
        UpdateUI();
    }

    public void CrystalMineUpgradeClicked()
    {
        _crystalMineLevel += 1;
        UpdateUI();
    }

    public void DeuteriumMineUpgradeClicked()
    {
        _deuteriumMineLevel += 1;
        UpdateUI();
    }

    private void LoadBuildingData()
    {
        _metalMineLevel = Database.LoadBuildingLevel(Database.Building.metalMine);
        _crystalMineLevel = Database.LoadBuildingLevel(Database.Building.crystalMine);
        _deuteriumMineLevel = Database.LoadBuildingLevel(Database.Building.deuteriumMine);

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
