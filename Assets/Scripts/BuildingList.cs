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

    void Start()
    {
        UpdateUI();
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

    private void UpdateUI()
    {
        _textMetalMine.text = "Metal Mine (Level " + _metalMineLevel + ")";
        _textCrystalMine.text = "Crystal Mine (Level " + _crystalMineLevel + ")";
        _textDeuteriumMine.text = "Deuterium Mine (Level " + _deuteriumMineLevel + ")";
    }

}
