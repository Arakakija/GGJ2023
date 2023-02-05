using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypePage{
    Boot,
    Light,
    Moon,
}

public class PageWordSelector : MonoBehaviour
{
    public TypePage typePage;
    public string word;

    public void SaveWord()
    {
        PlayerPrefs.SetString(typePage.ToString(),word);
    }
}


