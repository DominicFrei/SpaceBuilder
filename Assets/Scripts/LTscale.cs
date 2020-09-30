using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LTscale: MonoBehaviour
{
    public float initSpeed = 0.01f;
    public float scaleSpeed = 10f;

    private Vector3 startSize = new Vector3(0, 0, 0);
    private Vector3 normalSize = new Vector3(8, 8, 0);
    private Vector3 scaleMax = new Vector3(10, 10, 4);
    
    void Start()
    {
        ScaleZeroToSize();
    }

    void ScaleZeroToSize()
    {
        LeanTween.scale(gameObject, startSize, 0.01f);
        LeanTween.scale(gameObject, normalSize, 1f);
        LeanTween.scale(gameObject, scaleMax, 1f);
    }

}
