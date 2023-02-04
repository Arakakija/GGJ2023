using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzlePiece : DraggableObject
{
    public void ShowName()
    {
        Debug.Log("TEST " + gameObject.name);
    }
}
