using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField] private Text _textMetal = null;
    [SerializeField] private Text _textCrystal = null;
    [SerializeField] private Text _textDeuterium = null;

    private int _metal = 0;
    private int _crystal = 0;
    private int _deuterium = 0;

    void Start()
    {
        LoadResources();
        UpdateInterface();
        StartCoroutine(UpdateResourceCounter());
    }

    private IEnumerator UpdateResourceCounter()
    {
        while (true)
        {
            _metal += UnityEngine.Random.Range(0, 20);
            _crystal += UnityEngine.Random.Range(0, 10);
            _deuterium += UnityEngine.Random.Range(0, 5);

            UpdateInterface();
            SaveResources();

            yield return new WaitForSeconds(1.0f);
        }        
    }

    private void UpdateInterface()
    {
        _textMetal.text = "Metal: " + _metal;
        _textCrystal.text = "Crystal: " + _crystal;
        _textDeuterium.text = "Deuterium: " + _deuterium;
    }

    private void SaveResources()
    {
        PlayerPrefs.SetInt("metal", _metal);
        PlayerPrefs.SetInt("crystal", _crystal);
        PlayerPrefs.SetInt("deuterium", _deuterium);
    }

    private void LoadResources()
    {
        _metal = PlayerPrefs.GetInt("metal");
        _crystal = PlayerPrefs.GetInt("crystal");
        _deuterium = PlayerPrefs.GetInt("deuterium");
    }

}
