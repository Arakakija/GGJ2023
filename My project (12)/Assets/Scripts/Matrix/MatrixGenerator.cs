using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGenerator : MonoBehaviour
{
    private void Start()
    {
        MatrixController.Instance.InitGrid();
        MatrixController.Instance.ShuffleGrid();
    }
}
