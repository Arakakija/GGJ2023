using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour
{
    private GameObject selectedObject;
    private Vector3 offset;
    
    public void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Drag(mousePosition);
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
        Release();
    }

    void Drag(Vector3 mousePosition)
    {
        if (Input.GetMouseButtonDown(0) && Physics2D.OverlapPoint(mousePosition))
        {
            Collider2D[] results = Physics2D.OverlapPointAll(mousePosition);
            Collider2D highestCollider = GetHighestObject(results);
            selectedObject = highestCollider.transform.gameObject;
            offset = selectedObject.transform.position - mousePosition;
            
            var draggable = selectedObject.GetComponent<DraggableObject>();
            if (draggable) draggable.OnDrag?.Invoke();
            selectedObject.GetComponent<PuzzlePiece>().isDragging = true;
        }
    }
    
    Collider2D GetHighestObject(Collider2D[] results)
    {
        int highestValue = 0;
        Collider2D highestObject = results[0];
        for (var index = 0; index < results.Length; index++)
        {
            Collider2D col = results[index];
            Renderer ren = col.gameObject.GetComponent<Renderer>();
            if (ren && ren.sortingOrder > highestValue)
            {
                highestValue = ren.sortingOrder;
                highestObject = col;
            }
        }

        return highestObject;
    }

    void Release()
    {
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            var draggable = selectedObject.GetComponent<DraggableObject>();
            if (draggable) draggable.OnRelease?.Invoke();
            selectedObject = null;
        }
    }
    
}

