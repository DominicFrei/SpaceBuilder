using UnityEngine;
using UnityEngine.UI;

public class BuildingList : MonoBehaviour
{
    [SerializeField] private Text _textMetalMine = null;
    [SerializeField] private Text _textCrystalMine = null;
    [SerializeField] private Text _textDeuteriumMine = null;

    private BuildingsEntity _buildings = null;

    private void Start()
    {
        LoadBuildingData();
        UpdateUI();
    }

    private void OnApplicationQuit()
    {
        Database.SaveBuildings(_buildings);
    }

    public void MetalMineUpgradeClicked()
    {
        _buildings.MetalMine.Level += 1;
        UpdateUI();
    }

    public void CrystalMineUpgradeClicked()
    {
        _buildings.CrystalMine.Level += 1;
        UpdateUI();
    }

    public void DeuteriumMineUpgradeClicked()
    {
        _buildings.DeuteriumMine.Level += 1;
        UpdateUI();
    }

    private void LoadBuildingData()
    {
        _buildings = Database.LoadBuildings();
    }

    private void UpdateUI()
    {
        _textMetalMine.text = "Metal Mine (Level " + _buildings.MetalMine.Level + ")";
        _textCrystalMine.text = "Crystal Mine (Level " + _buildings.CrystalMine.Level + ")";
        _textDeuteriumMine.text = "Deuterium Mine (Level " + _buildings.DeuteriumMine.Level + ")";
    }
}
