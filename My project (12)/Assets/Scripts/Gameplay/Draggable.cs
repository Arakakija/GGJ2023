using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DraggableObject : MonoBehaviour
{
     public UnityEvent onDrag;
     public UnityEvent onRelease;
}