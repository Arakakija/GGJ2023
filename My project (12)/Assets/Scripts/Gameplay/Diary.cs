using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : Clickeable
{
    void OnEnable()
    {
        onClick += OpenDiary;
    }
    
    void OpenDiary()
    {
        if (MatrixController.Instance.isPlaying) return;
        UIManager.OnOpenDiary?.Invoke();
    }
}
