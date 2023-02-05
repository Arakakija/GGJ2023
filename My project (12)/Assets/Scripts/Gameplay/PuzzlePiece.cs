    using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PuzzlePiece : Draggable
{
    public Vector2 CorrectPosition;
    public Vector2 CurrentPostion;

    public Vector3 startPosition;
    
    public GameObject objectToSwap;

    private SpriteRenderer _renderer;

    private void OnEnable()
    {
        DragEndObject += OnDrop;
    }
    private void OnDisable()
    {
        DragEndObject -= OnDrop;
    }

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other == null || !isDragged ||!other.GetComponent<PuzzlePiece>() || other.GetComponent<PuzzlePiece>().CorrectPosition.x != CorrectPosition.x) return;
        objectToSwap = other.gameObject;
    }

    public bool IsOnCorrectPosition()
    {
        return CurrentPostion == CorrectPosition;
    }

    public void OnDrop(Draggable draggable)
    {
        if (objectToSwap == null)
        {
            transform.position = MatrixController.Instance.SetPosition(CurrentPostion);
            return;
        }
        MatrixController.Instance.SwapTiles(CurrentPostion,objectToSwap.GetComponent<PuzzlePiece>().CurrentPostion);
        MatrixController.Instance.TryFreezeRow(this);
        MatrixController.Instance.TryFreezeRow(objectToSwap.GetComponent<PuzzlePiece>());
        objectToSwap = null;
    }
    
    public IEnumerator FadeImage(bool fadeAway)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                _renderer.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                _renderer.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
    
}
