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

    private void Start()
    {
        if (soundsNames != null)
        {
            soundsNames = null;
        }

        soundsNames = new string[3];
        soundsNames[0] = "Pagina01";
        soundsNames[1] = "Pagina02";
        soundsNames[2] = "Pagina03";
    }

    void PickUpPage()
    {
        PlayerController.Instance.inventory.AddPage(this);
        Destroy(gameObject);
    }
}
