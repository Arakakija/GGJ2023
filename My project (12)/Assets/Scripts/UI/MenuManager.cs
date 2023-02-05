using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioSource _as;
    [SerializeField] VideoPlayer creditsVideo;

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject creditsPanel;

    [SerializeField] string menuSoundName;
    [SerializeField] string onClickSoundName;

    // Start is called before the first frame update
    void Start()
    {
        
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
        SoundControler.Instance.PlaySound(onClickSoundName, _as);
        StartCoroutine(LoadAsynchronously(1));
    }

    public void OnCredits()
    {
        SoundControler.Instance.PlaySound(onClickSoundName, _as);
        menuPanel.SetActive(false);
        creditsPanel.SetActive(true);

    }

    public void OnExit()
    {
        SoundControler.Instance.PlaySound(onClickSoundName, _as);
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
