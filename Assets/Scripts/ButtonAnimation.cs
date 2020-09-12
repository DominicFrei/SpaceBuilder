using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonAnimation : MonoBehaviour
{
    //variables
    public Sprite btnUp;
    public Sprite btnDown;
    public Sprite btnHover;
    public UnityEvent buttonClick;

    void Awake()
    {
        if (buttonClick == null) { buttonClick = new UnityEvent(); }
    }

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = btnDown;
    }
    void OnMouseUp()
    {
        buttonClick.Invoke();
        GetComponent<SpriteRenderer>().sprite = btnUp;
    }
}
