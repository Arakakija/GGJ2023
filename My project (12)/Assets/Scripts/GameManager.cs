using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    const string bootWord = "Berazategui";
    const string moonWord = "disappeared";
    const string ligthWord = "refuse";

    float timer;

    [SerializeField] GameObject winPanel;

    bool win = false;

    [SerializeField] string musicName;

    private void Start()
    {
        timer = 30f;
    }

    private void Update()
    {
        if (win) timer -= Time.deltaTime;
        if (win && Input.GetKeyDown(KeyCode.Escape) || timer <= 0)
        {
            Application.Quit();
        }
    }
    public void StartTheme()
    {
        SoundControler.Instance.SetMusic(musicName);
        SoundControler.Instance.SetVolMusic(0.4f);
    }

    public void CheckWin()
    {
        if (PlayerPrefs.GetString(TypePage.Boot.ToString()) == bootWord &&
            PlayerPrefs.GetString(TypePage.Moon.ToString()) == moonWord &&
            PlayerPrefs.GetString(TypePage.Light.ToString()) == ligthWord)
        {
            win = true;
            winPanel.SetActive(true);
            Debug.Log("WIN");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.DeleteAll();
            PlayerController.Instance.ResetPlayer();
            UIManager.Instance.ResetUI();
            StartTheme();
        }
    }
}
