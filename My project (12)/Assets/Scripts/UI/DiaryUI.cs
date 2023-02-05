using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiaryUI : MonoBehaviour
{
    public static UnityAction OnOpenDiary;
    public static UnityAction OnCloseDiary;
    
    [SerializeField] private Image[] diaryPages;

    [SerializeField] private Button openDiaryButton,nextButton,prevButton;

    [SerializeField] private GameObject panelButton;
    [SerializeField] private GameObject panelPage;

    public int currentPage = 0;

    private void OnEnable()
    {
        OnOpenDiary += OpenDiaryUI;
        OnCloseDiary += CloseDiaryUI;
    }

    public void NextPage()
    {
        if (!prevButton.enabled) prevButton.enabled = true;
        diaryPages[currentPage].gameObject.SetActive(false);
        diaryPages[currentPage + 1].gameObject.SetActive(true);
        currentPage++;
        
        if (currentPage >= diaryPages.Length - 1)
        {
            nextButton.enabled = false;
        }
    }

    public void OpenDiaryUI()
    {
        panelPage.SetActive(true);
        diaryPages[0].gameObject.SetActive(true);
        openDiaryButton.gameObject.SetActive(true);
        panelButton.SetActive(false);
    }

    public void GoFirstPage()
    {
        NextPage();
        if (!nextButton.enabled) nextButton.enabled = true;
        panelButton.SetActive(true);
        openDiaryButton.gameObject.SetActive(false);
    }

    public void CloseDiaryUI()
    {
        foreach (var page in diaryPages)
        {
            page.gameObject.SetActive(false);
        }
        panelButton.SetActive(false);
        openDiaryButton.gameObject.SetActive(false);
        currentPage = 0;
    }
    

    public void PrevPage()
    {
        if (!nextButton.enabled) nextButton.enabled = true;
        diaryPages[currentPage].gameObject.SetActive(false);
        diaryPages[currentPage - 1].gameObject.SetActive(true);
        currentPage--;
        
        if (currentPage <= 0)
        {
            prevButton.enabled = false;
            openDiaryButton.gameObject.SetActive(true);
            panelButton.SetActive(false);
        }
    }
    
}
