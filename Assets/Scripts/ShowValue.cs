using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowValue : MonoBehaviour
{
    TextMeshProUGUI percentageText;

    void Start()
    {
        percentageText = GetComponent<TextMeshProUGUI>();
    }
    public void TextUpdate(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }
}
