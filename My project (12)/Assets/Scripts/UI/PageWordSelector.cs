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
        PlayRandSound();
    }
    void PlayRandSound()
    {
        int rand = UnityEngine.Random.Range(0, 3);

        switch (rand) 
        {
            case 0:
                SoundControler.Instance.PlaySound("Lapiz01");
                break;

            case 1:
                SoundControler.Instance.PlaySound("Lapiz02");
                break;

            case 2:
                SoundControler.Instance.PlaySound("Lapiz03");
                break;

            default:
                Debug.LogError($"El numero {rand} esta fuera de los parametros.");
                break;
        }
    }
}


