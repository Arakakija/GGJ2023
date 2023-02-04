using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PuzzlePiece : DraggableObject
{
    private List<PuzzlePiece> _list = new List<PuzzlePiece>();
    
    private Vector3 _startPosition;
    private Vector3 newPosition;

    public int yPosition;
    
    [SerializeField]
    private GameObject objectToSwap;

    private SpriteRenderer _spriteRenderer;

    public bool isDragging = false;

    private void OnEnable()
    {
        OnRelease += SwapPosition;
        OnDrag += GetStartPosition;
    }
    
    private void OnDisable()
    {
        OnRelease -= SwapPosition;
        OnDrag -= GetStartPosition;
    }


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject || !isDragging) return;
        if(other.GetComponent<PuzzlePiece>()) _list.Add(other.GetComponent<PuzzlePiece>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(_list.Contains(other.GetComponent<PuzzlePiece>())) _list.Remove(other.GetComponent<PuzzlePiece>());
    }

    void GetStartPosition()
    {
        _startPosition = transform.position;
    }
    
    void SwapPosition()
    {
        objectToSwap = _list.Count > 0 ? _list[0].gameObject : null;
        
        if (!objectToSwap || objectToSwap.GetComponent<PuzzlePiece>().yPosition != this.yPosition)
        {
            transform.position = _startPosition;
            return;
        }

        newPosition = objectToSwap.transform.position;
        transform.position = newPosition;
        objectToSwap.transform.position = _startPosition;

        objectToSwap = null;
        isDragging = false;
    }
}
