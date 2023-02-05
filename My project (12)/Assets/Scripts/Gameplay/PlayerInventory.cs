using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<PageItem> pages = new List<PageItem>();
    

    public void AddPage(PageItem page)
    {
        pages.Add(page);
        if (hasAllPages())
        {
            MatrixController.Instance.OnStartPartiture?.Invoke();
        }
    }
    
    public int PagesCount()
    {
        return pages.Count;
    }

    public bool hasAllPages()
    {
        return PagesCount() >= 8;
    }

    public void ResetInventory()
    {
        pages.Clear();
    }
}
