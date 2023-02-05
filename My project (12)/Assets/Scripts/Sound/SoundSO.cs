using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameOfSound", menuName = "New Sound")]

public class SoundSO : ScriptableObject
{
    public string _name;
    public AudioClip audio;
}
