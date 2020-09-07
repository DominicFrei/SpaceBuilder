using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingList : MonoBehaviour
{
    [SerializeField] private Text _textMetalMine = null;
    [SerializeField] private Text _textCrystalMine = null;
    [SerializeField] private Text _textDeuteriumMine = null;


    // Start is called before the first frame update
    void Start()
    {
        _textMetalMine.text = "Metal Mine (Level 42)";
        _textCrystalMine.text = "Crystal Mine (Level 42)";
        _textDeuteriumMine.text = "Deuterium Mine (Level 42)";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
