using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CellController : MonoBehaviour
{
    public bool isRed { get; set; }
    public bool clicked { get; set; }
    public Action<CellController> onClick;

    public void SetColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }

    void OnMouseDown()
    {
        onClick(this);
    }   
}
