using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class Draggable : MonoBehaviour
{
    protected UnityAction<Draggable> DragEndObject;
    public bool isDragged = false;
    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;
    public bool isDraggable = true;

    private void OnMouseDown()
    {
        if(!isDraggable) return;
        isDragged = true;
        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
    }

    private void OnMouseDrag()
    {
        if (isDragged && isDraggable)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            transform.localPosition = spriteDragStartPosition +
                                      (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
        }
    }

    private void OnMouseUp()
    {
        isDragged = false;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        DragEndObject?.Invoke(this);
    }

    public void Lock()
    {
        isDraggable = false;
    }
    


}