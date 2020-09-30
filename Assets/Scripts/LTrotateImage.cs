using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class LTrotateImage : MonoBehaviour
{
    public float rotspeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.rotateAround(gameObject, Vector3.forward, 360, rotspeed);
        LeanTween.rotateAroundLocal(gameObject, Vector3.forward, 360, rotspeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
