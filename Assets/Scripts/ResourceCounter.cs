using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] private Text _textMetal = null;
    [SerializeField] private Text _textCrystal = null;
    [SerializeField] private Text _textDeuterium = null;

    private int _metal = 0;
    private int _crystal = 0;
    private int _deuterium = 0;

    private readonly WaitForSeconds _resourcesUpdateInterval = new WaitForSeconds(1.0f);

    private void Start()
    {
        LoadResources();
        UpdateInterface();
        StartCoroutine(UpdateResourceCounter());
    }

    private void OnApplicationQuit()
    {
        PlayerPrefsHelper.SaveResources(_metal, _crystal, _deuterium);
        PlayerPrefsHelper.SaveLastUpdateDate();
    }

    private IEnumerator UpdateResourceCounter()
    {
        while (true)
        {
            _metal += UnityEngine.Random.Range(0, 20);
            _crystal += UnityEngine.Random.Range(0, 10);
            _deuterium += UnityEngine.Random.Range(0, 5);
            UpdateInterface();
                        
            yield return _resourcesUpdateInterval;
        }
    }

    private void UpdateInterface()
    {
        _textMetal.text = "Metal: " + _metal;
        _textCrystal.text = "Crystal: " + _crystal;
        _textDeuterium.text = "Deuterium: " + _deuterium;
    }

    private void LoadResources()
    {
        (int metal, int crystal, int deuterium) = PlayerPrefsHelper.LoadResources();
        
        DateTime? lastUpdateDate = PlayerPrefsHelper.LoadLastUpdateDate();
        int secondsSinceLastUpdate = 0;
        if (null != lastUpdateDate)
        {
            secondsSinceLastUpdate = DateHelper.DifferenceToNowInSeconds(lastUpdateDate);
        }        

        _metal = metal + secondsSinceLastUpdate;
        _crystal = crystal + secondsSinceLastUpdate;
        _deuterium = deuterium + secondsSinceLastUpdate;

        Debug.Log("Resources added: " + secondsSinceLastUpdate);
    }

}
