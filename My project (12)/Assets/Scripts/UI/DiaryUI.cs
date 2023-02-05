using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiaryUI : Singleton<DiaryUI>
{
    public static UnityAction OnOpenDiary;
    public static UnityAction OnCloseDiary;
    
    [SerializeField] private Image[] diaryPages;

    [SerializeField] private Button openDiaryButton, submitButton;

    [SerializeField] private GameObject panelButton;
    [SerializeField] private GameObject panelPage;

    [SerializeField] private NoteWords[] _noteWords;

    public Image prevPage;
    public Image nextPage;

    public int currentPage = 0;

    private void OnEnable()
    {
        OnOpenDiary += OpenDiaryUI;
        OnCloseDiary += CloseDiaryUI;
    }

    private void Start()
    {
        panelPage = GameObject.FindWithTag("Pages");
    }

    public void NextPage()
    {
        if(currentPage >= 4) return;
        if(!nextPage.enabled) nextPage.enabled = true;
        if(!prevPage.enabled) prevPage.enabled = true;
        diaryPages[currentPage].gameObject.SetActive(false);
        diaryPages[currentPage + 1].gameObject.SetActive(true);
        currentPage++;

        if (currentPage >= 4)
        {
            RefreshNoteWords();
            if (_noteWords.All(x => !String.IsNullOrEmpty(x.text.text))) submitButton.gameObject.SetActive(true);
            nextPage.enabled = false;
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
        panelButton.SetActive(true);
        nextPage.enabled = true;
        prevPage.enabled = false;
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
        submitButton.gameObject.SetActive(false);
        currentPage = 0;
    }
    

    public void PrevPage()
    {
        if(!nextPage.enabled) nextPage.enabled = true;
        if(!prevPage.enabled) prevPage.enabled = true;
        diaryPages[currentPage].gameObject.SetActive(false);
        diaryPages[currentPage - 1].gameObject.SetActive(true);
        currentPage--;
        
        if (currentPage <= 0)
        {
            prevPage.enabled = false;
            openDiaryButton.gameObject.SetActive(true);
            panelButton.SetActive(false);
        }
    }


    void RefreshNoteWords()
    {
        foreach (var note in _noteWords)
        {
            note.gameObject.SetActive(true);
        }
    }

    void ResetNoteWords()
    {
        foreach (var note in _noteWords)
        {
            note.text.text = "";
        }
    }

    public void ResetUI()
    {
        CloseDiaryUI();
        ResetNoteWords();
    }
}
