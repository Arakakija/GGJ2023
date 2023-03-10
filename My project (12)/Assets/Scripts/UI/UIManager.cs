using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public static UnityAction OnOpenDiary;
    public static UnityAction OnCloseDiary;
    
    [SerializeField] private Image[] diaryPages;

    [SerializeField] private Button openDiaryButton, submitButton;

    [SerializeField] private GameObject panelButton;
    [SerializeField] private GameObject panelPage;
    [SerializeField] private GameObject panelPartitures;
    [SerializeField] private GameObject closePanel;

    [SerializeField] private NoteWords[] _noteWords;

    [SerializeField] string[] soundsNames;

    public Image prevPage;
    public Image nextPage;

    public int currentPage = 0;

    public bool isDiaryOpen = false;

    private void OnEnable()
    {
        OnOpenDiary += OpenDiaryUI;
        OnCloseDiary += CloseDiaryUI;
    }

    private void Start()
    {
        MatrixController.Instance.OnStartPartiture += ShowPartituresUI;
        MatrixController.Instance.OnCompleted += HidePartituresUI;
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

        PlayRandSound();

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
        closePanel.SetActive(true);
        diaryPages[0].gameObject.SetActive(true);
        openDiaryButton.gameObject.SetActive(true);
        panelButton.SetActive(false);
        isDiaryOpen = true;
    }

    public void GoFirstPage()
    {
        NextPage();
        PlayRandSound();
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

        PlayRandSound();

        panelButton.SetActive(false);
        closePanel.SetActive(false);
        openDiaryButton.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        currentPage = 0;
        isDiaryOpen = false;
    }
    

    public void PrevPage()
    {
        if(!nextPage.enabled) nextPage.enabled = true;
        if(!prevPage.enabled) prevPage.enabled = true;
        diaryPages[currentPage].gameObject.SetActive(false);
        diaryPages[currentPage - 1].gameObject.SetActive(true);
        currentPage--;
        
        PlayRandSound();

        if (currentPage <= 0)
        {
            prevPage.enabled = false;
            openDiaryButton.gameObject.SetActive(true);
            panelButton.SetActive(false);
        }
    }


    public void ShowPartituresUI()
    {
        panelPartitures.gameObject.SetActive(true);
    }
    
    public void HidePartituresUI()
    {
        panelPartitures.gameObject.SetActive(false);
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
        HidePartituresUI();
    }

    void PlayRandSound()
    {
        if (soundsNames == null) return;
        if (soundsNames.Length <= 0) return;

        int rand = UnityEngine.Random.Range(0, soundsNames.Length - 1);

        SoundControler.Instance.PlaySound(soundsNames[rand]);
    }
}
