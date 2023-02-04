using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class DraggableObject : MonoBehaviour
{
     public UnityAction OnDrag;
     public UnityAction OnRelease;
}