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
        DiaryUI.OnOpenDiary?.Invoke();
    }
}
