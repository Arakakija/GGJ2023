using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.String;

public class NoteWords : MonoBehaviour
{
    public TypePage type;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (IsNullOrEmpty(PlayerPrefs.GetString(type.ToString())))
        {
            gameObject.SetActive(false);
            return;
        }
        text.text = PlayerPrefs.GetString(type.ToString());
    }
}
