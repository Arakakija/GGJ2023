using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioSource _as;

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject creditsPanel;

    [SerializeField] string menuSoundName;
    [SerializeField] string onClickSoundName;

    // Start is called before the first frame update
    void Start()
    {
        SoundControler.Instance.SetMusic(menuSoundName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Back();
    }

    void Back()
    {
        if (menuPanel.activeSelf) OnExit();
        if (creditsPanel.activeSelf)
        {
            menuPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
    }

    public void OnPlay()
    {
        SoundControler.Instance.PlaySound(onClickSoundName);
        StartCoroutine(LoadAsynchronously(1));
    }

    public void OnCredits()
    {
        SoundControler.Instance.PlaySound(onClickSoundName);
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);

    }

    public void OnExit()
    {
        SoundControler.Instance.PlaySound(onClickSoundName);
        Application.Quit();
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            //loadingBar.value = progress;

            yield return null;
        }


    }
}
