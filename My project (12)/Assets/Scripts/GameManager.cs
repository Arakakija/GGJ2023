using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    const string bootWord = "Berazategui";
    const string moonWord = "disappeared";
    const string ligthWord = "refuse";

    [SerializeField] string musicName;

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
