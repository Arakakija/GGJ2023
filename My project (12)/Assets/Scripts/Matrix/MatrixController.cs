using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MatrixController : Singleton<MatrixController>
{
    public static UnityAction<float> checkRow;
    public static UnityAction<PuzzlePiece> checkPiecePosition;
    public PuzzlePiece[,] matrix = new PuzzlePiece[3, 3];
    
    [SerializeField] public float _offset;
    
    private bool matchFound = false;

    private void OnEnable()
    {
        checkPiecePosition += CheckPiecePosition;
    }

    private void OnDisable()
    {
        checkPiecePosition -= CheckPiecePosition;
    }

    void CheckPiecePosition(PuzzlePiece piece)
    {
        ClearAllMatches(piece);
    }
    
    
    private void ClearMatch(PuzzlePiece piece,Vector2[] paths) // 1
    {
        List<GameObject> matchingTiles = new List<GameObject>(); // 2
        for (int i = 0; i < paths.Length; i++) // 3
        {
            matchingTiles.AddRange(piece.FindMatch(paths[i]));
        }
        if (matchingTiles.Count >= 2) // 4
        {
            for (int i = 0; i < matchingTiles.Count; i++) // 5
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            matchFound = true; // 6
        }
    }
    
    public void ClearAllMatches(PuzzlePiece piece) {
        if (piece == null)
            return;

        ClearMatch(piece,new Vector2[2] { Vector2.left, Vector2.right });
        if (matchFound) {
            matchFound = false;
            FreezeRow(piece.correctOrder - 1);
        }
    }


    public void FreezeRow(float rowNumber)
    {
        var row =new CustomArray<PuzzlePiece>().GetColumn(matrix, (int) rowNumber);
        foreach (var tile in row)
        {
            tile.Lock();
        }
    }
    


}
