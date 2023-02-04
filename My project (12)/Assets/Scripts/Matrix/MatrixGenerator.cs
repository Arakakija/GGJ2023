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
    
    private int[,] matrix = new int[3,3];


    [SerializeField] private float _offset;

    public void Start()
    {
        GenerateMatrix();
    }

    void GenerateMatrix()
    {
        for (var index0 = 0; index0 < matrix.GetLength(0); index0++)
        {
            for (var index1 = 0; index1 < matrix.GetLength(1); index1++)
            {
                GameObject go = Instantiate(_prefab, SetPosition(_parentTransform.position,index0,index1), Quaternion.identity, _parentTransform);
            }
        }

    }

    Vector3 SetPosition(Vector3 parentPosition,int x, int y)
    {
        return new Vector3(parentPosition.x + x * _offset, (parentPosition.y +y *_offset));
    }
        
    
}
