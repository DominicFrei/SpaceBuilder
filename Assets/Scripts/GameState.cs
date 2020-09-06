using System;
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
            PlayerPrefsHelper.SaveResources(_metal, _crystal, _deuterium);
            PlayerPrefsHelper.SaveLastUpdateDate();

            yield return new WaitForSeconds(1.0f);
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
        int secondsSinceLastUpdate = SecondsSinceLastUpdate();

        _metal = metal + secondsSinceLastUpdate;
        _crystal = crystal + secondsSinceLastUpdate;
        _deuterium = deuterium + secondsSinceLastUpdate;

        Debug.Log("Resources added: " + secondsSinceLastUpdate);
    }

    

    private int SecondsSinceLastUpdate()
    {
        DateTime? lastUpdateDate = PlayerPrefsHelper.LoadLastUpdateDate();
        if (null == lastUpdateDate)
        {
            return 0;
        }

        DateTime now = DateTime.Now.ToUniversalTime();
        DateTime unwrappedLastUpdateDate = lastUpdateDate ?? now;
        TimeSpan difference = now - unwrappedLastUpdateDate;
        int differenceInSeconds = (int)difference.TotalSeconds;
        return differenceInSeconds;
    }

}
