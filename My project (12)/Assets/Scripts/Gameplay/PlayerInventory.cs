using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<PageItem> pages;
    
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
}
