using System;
using System.Collections;
using System.Collections.Generic;
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
    private DateTime _lastUpdate = null;

    void Start()
    {
        _metal = PlayerPrefs.GetInt("metal");
        _crystal = PlayerPrefs.GetInt("crystal");
        _deuterium = PlayerPrefs.GetInt("deuterium");

        _textMetal.text = "Metal: " + _metal;
        _textCrystal.text = "Crystal: " + _crystal;
        _textDeuterium.text = "Deuterium: " + _deuterium;

        StartCoroutine(UpdateResourceCounter());
    }

    private IEnumerator UpdateResourceCounter()
    {
        while (true)
        {
            _metal += UnityEngine.Random.Range(1, 11);
            _crystal += Random.Range(1, 6);
            _deuterium += Random.Range(1, 3);

            _textMetal.text = "Metal: " + _metal;
            _textCrystal.text = "Crystal: " + _crystal;
            _textDeuterium.text = "Deuterium: " + _deuterium;

            PlayerPrefs.SetInt("metal", _metal);
            PlayerPrefs.SetInt("crystal", _crystal);
            PlayerPrefs.SetInt("deuterium", _deuterium);

            yield return new WaitForSeconds(1.0f);
        }        
    }

}
