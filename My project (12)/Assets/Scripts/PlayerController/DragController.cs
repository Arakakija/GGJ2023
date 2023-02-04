using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : MonoBehaviour
{
    private GameObject selectedObject; 
    private Vector3 offset;
    
    public void Start()
    {
        
    }
    
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
        }
    }
    
    Collider2D GetHighestObject(Collider2D[] results)
    {
        int highestValue = 0;
        Collider2D highestObject = results[0];
        foreach(Collider2D col in results)
        {
            Renderer ren = col.gameObject.GetComponent<Renderer>();
            if(ren && ren.sortingOrder > highestValue)
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
            if (draggable) draggable.onDrag?.Invoke();
            selectedObject = null;
        }
    }
}

