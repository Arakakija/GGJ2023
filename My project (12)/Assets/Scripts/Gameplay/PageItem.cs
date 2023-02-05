using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageItem : Clickeable
{
    private void OnEnable()
    {
        onClick += PickUpPage;
    }

    void PickUpPage()
    {
        PlayerController.Instance.inventory.AddPage(this);
        Destroy(gameObject);
    }
}
