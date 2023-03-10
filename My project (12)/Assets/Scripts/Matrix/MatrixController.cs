using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MatrixController : Singleton<MatrixController>
{
    public UnityAction OnStartPartiture;
    public UnityAction OnCompleted;
    public UnityAction OnRowCompleted;
    
    
    public Transform _placeholder;
    public GameObject tile;
    public CanvasGroup canvasGroup;
    private int dimension = 3;
    public float DistanceX = 1.0f; 
    public float DistanceY = 1.0f;
    private GameObject[,] Grid;

    [SerializeField] private TextMeshProUGUI[] texts;

    public int lockedRow = 0;

    public bool isPlaying = false;

    private void OnEnable()
    {
        OnStartPartiture += InitPartitures;
        OnCompleted += PuzzleCompleted;
    }

    private void InitPartitures()
    {
        InitGrid();
        ShuffleGrid();
        isPlaying = true;
    }
    
    public void InitGrid()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Partitures");
        Grid = new GameObject[3,3];
        int i = 0;
        Vector3 positionOffset= transform.position - new Vector3(dimension * DistanceX / 2.0f, dimension * DistanceY / 2.0f, 0);
        for (int row = 0; row < dimension; row++)
        {
            for (int column = 0; column < dimension; column++) 
            {
                GameObject newTile = Instantiate(tile,_placeholder); 
                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>(); 
                renderer.sprite = sprites[i];
                newTile.transform.parent = _placeholder; 
                newTile.transform.position = new Vector3(column * DistanceX, row * DistanceY, 0) + positionOffset;
                var newPiece = newTile.GetComponent<PuzzlePiece>();
                newPiece.CorrectPosition = new Vector2(column, row);
                newPiece.CurrentPostion = newPiece.CorrectPosition;
                Grid[column, row] = newTile;
                i++;
            }
        }
    }
    
    public void ShuffleGrid()
    {
        foreach (var tile in Grid)
        {
            int randX = Random.Range(0, 3);
            SwapTiles(tile.GetComponent<PuzzlePiece>().CurrentPostion, GetObjectAt( (int)tile.GetComponent<PuzzlePiece>().CurrentPostion.x,randX).CurrentPostion);
        }
    }


    public bool CheckNeighbours(PuzzlePiece piece)
    {
        var RightNeigbour = GetObjectAt((int)piece.CurrentPostion.x + 1, (int)piece.CurrentPostion.y );
        var LeftNeigbour = GetObjectAt((int)piece.CurrentPostion.x -1 , (int)piece.CurrentPostion.y );

        if (RightNeigbour && LeftNeigbour && LeftNeigbour.IsOnCorrectPosition() &&
            RightNeigbour.IsOnCorrectPosition() && piece.IsOnCorrectPosition())
        {
            return true;
        }

        if (!RightNeigbour && LeftNeigbour && LeftNeigbour.IsOnCorrectPosition() && piece.IsOnCorrectPosition() &&
            CheckNeighbours(LeftNeigbour))
        {
            return true;
        }

        if (RightNeigbour && !LeftNeigbour && RightNeigbour.IsOnCorrectPosition() && piece.IsOnCorrectPosition() &&
            CheckNeighbours(RightNeigbour))
        {
            return true;
        }

        return false;
    }
    
    PuzzlePiece GetObjectAt(int column, int row)
    {
        if (column < 0 || column >= dimension
                       || row < 0 || row >= dimension)
            return null;
        GameObject tile = Grid[column, row];
        PuzzlePiece draggable = tile.GetComponent<PuzzlePiece>();
        return draggable;
    }
    
    public void SwapTiles(Vector2 tile1Position, Vector2 tile2Position) // 1
    {
        Vector2 positionOffset = transform.position - new Vector3(dimension * DistanceX / 2.0f, dimension * DistanceY / 2.0f, 0);
        GameObject tile1 = Grid[(int)tile1Position.x, (int)tile1Position.y];
        GameObject tile2 = Grid[(int)tile2Position.x, (int)tile2Position.y];

        tile1.transform.position = new Vector2(tile2Position.x * DistanceX,tile2Position.y * DistanceY) + positionOffset;
        tile2.transform.position = new Vector2(tile1Position.x * DistanceX,tile1Position.y * DistanceY) + positionOffset;;

        var piece1 = tile1.GetComponent<PuzzlePiece>();
        var piece2 = tile2.GetComponent<PuzzlePiece>();

        (piece1.CurrentPostion, piece2.CurrentPostion) = (piece2.CurrentPostion, piece1.CurrentPostion);
        
        (Grid[(int)tile1Position.x, (int)tile1Position.y], Grid[(int)tile2Position.x, (int)tile2Position.y]) = (Grid[(int)tile2Position.x, (int)tile2Position.y], Grid[(int)tile1Position.x, (int)tile1Position.y]);
    }

    public Vector3 SetPosition(Vector2 CurrentPosition)
    {
        Vector3 positionOffset = transform.position - new Vector3(dimension * DistanceX / 2.0f, dimension * DistanceY / 2.0f, 0);
        return new Vector3(CurrentPosition.x * DistanceX,CurrentPosition.y * DistanceY) + positionOffset;
    }

    public void TryFreezeRow(PuzzlePiece piece)
    {
        if(!CheckNeighbours(piece)) return;
        foreach (var tile in Grid)
        {
            if ((int)tile.GetComponent<PuzzlePiece>().CorrectPosition.y == (int)piece.CorrectPosition.y)
            {
                tile.GetComponent<PuzzlePiece>().Lock();
            }
        }
        OnRowCompleted?.Invoke();
        lockedRow++;
        if(lockedRow >= 3) OnCompleted?.Invoke();
    }


    public void HidePartitures()
    {
        _placeholder.gameObject.SetActive(false);
        isPlaying = false;
    }

    public void ShowPartitures()
    {
        if(UIManager.Instance.isDiaryOpen) return;
        _placeholder.gameObject.SetActive(true);
        isPlaying = true;
    }

    void PuzzleCompleted()
    {
        StartCoroutine(StartFade());
        isPlaying = false;
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(1.5f);
        foreach (var tile in Grid)
        {
            StartCoroutine(tile.GetComponent<PuzzlePiece>().FadeImage(true));
        }
    }
}
