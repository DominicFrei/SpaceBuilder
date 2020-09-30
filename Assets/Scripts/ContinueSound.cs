using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueSound : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
