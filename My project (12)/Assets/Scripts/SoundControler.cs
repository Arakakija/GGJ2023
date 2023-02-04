using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControler : MonoBehaviour
{
    SoundControler instance;
    public SoundControler Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this);
    }

    Dictionary<string, AudioClip> soundTracks;
    Dictionary<string, AudioClip> soundEffects;

    AudioSource audioSource;

    float sfxVolume;

    private void Start()
    {
        sfxVolume = 0;
        if (gameObject.GetComponent<AudioSource>()) audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void SetVolMusic(double vol)
    {
        if (audioSource == null) return;
        audioSource.volume = (float)vol;
    }

    public void SetVolSFX(double vol)
    {
        if (audioSource == null) return;
        sfxVolume = (float)vol;
    }

    public void SetMusic(string name)
    {
        if (audioSource == null) return;
        soundTracks.TryGetValue(name, out AudioClip value);
        audioSource.clip = value;
    }

    public void PlayPauseMusic(bool isOn)
    {
        if (audioSource == null) return;
        if (isOn)
        {
            audioSource.Play();
        }
        else audioSource.Pause();
    }

    public void StopMusic()
    {
        if (audioSource == null) return;
        audioSource.Stop();
    }

    public void PlaySound(string name, AudioSource _as)
    {
        if (_as == null) return;

        soundTracks.TryGetValue(name, out AudioClip value);

        _as.clip = value;
        _as.volume = sfxVolume;
        _as.Play();
    }
}
