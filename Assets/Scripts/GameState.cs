using System;
using System.Collections;
using System.Globalization;
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

    private readonly String _keyMetal = "key.metal";
    private readonly String _keyCrystal = "key.crystal";
    private readonly String _keyDeuterium = "key.deuterium";
    private readonly String _keyLastUpdate = "key.lastUpdate";

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
            SaveLastUpdateDate();

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
        PlayerPrefs.SetInt(_keyMetal, _metal);
        PlayerPrefs.SetInt(_keyCrystal, _crystal);
        PlayerPrefs.SetInt(_keyDeuterium, _deuterium);
    }

    private void LoadResources()
    {
        _metal = PlayerPrefs.GetInt(_keyMetal);
        _crystal = PlayerPrefs.GetInt(_keyCrystal);
        _deuterium = PlayerPrefs.GetInt(_keyDeuterium);

        int secondsSinceLastUpdate = SecondsSinceLastUpdate();

        _metal += secondsSinceLastUpdate;
        _crystal += secondsSinceLastUpdate;
        _deuterium += secondsSinceLastUpdate;

        Debug.Log("Resources added: " + secondsSinceLastUpdate);
    }

    private void SaveLastUpdateDate()
    {
        DateTime now = DateTime.Now;
        DateTime nowUTC = now.ToUniversalTime();
        String nowUTCString = nowUTC.ToString("O", CultureInfo.InvariantCulture);
        PlayerPrefs.SetString(_keyLastUpdate, nowUTCString);
    }

    private int SecondsSinceLastUpdate()
    {
        String lastUpdateUTCString = PlayerPrefs.GetString(_keyLastUpdate);

        if ("" == lastUpdateUTCString)
        {
            Debug.LogWarning("Could not read lastUpdate. Probably never saved before.");
            return 0;
        }

        TimeZoneInfo.ClearCachedData(); // just in case the time zone has changed
        DateTime lastUpdateDate;
        bool dateParsedSuccessfully = DateTime.TryParseExact(lastUpdateUTCString, "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out lastUpdateDate);

        if (!dateParsedSuccessfully)
        {
            Debug.LogError("Could not parse saved date.");
            return 0;
        }

        DateTime now = DateTime.Now.ToUniversalTime();
        TimeSpan difference = now - lastUpdateDate;
        int differenceInSeconds = (int)difference.TotalSeconds;
        return differenceInSeconds;
    }

}
