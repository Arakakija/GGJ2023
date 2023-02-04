using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Transform _parentTransform;

    public void Start()
    {
        GenerateMatrix();
    }

    void GenerateMatrix()
    {
        
        for (var index0 = 0; index0 < MatrixController.Instance.matrix.GetLength(0); index0++)
        {
            int i = 1;
            for (var index1 = 0; index1 <MatrixController.Instance.matrix.GetLength(1); index1++)
            {
                PuzzlePiece go = Instantiate(_prefab, SetPosition(_parentTransform.position,index0,index1), Quaternion.identity, _parentTransform).GetComponent<PuzzlePiece>();
                go.yPosition = index0;
                go.correctOrder = i;
                MatrixController.Instance.matrix[index0, index1] = go;
                i++;
            }
        }

    }

    Vector3 SetPosition(Vector3 parentPosition,int x, int y)
    {
        return new Vector3(parentPosition.x + x * MatrixController.Instance._offset, (parentPosition.y +y * MatrixController.Instance._offset));
    }
        
    
}
