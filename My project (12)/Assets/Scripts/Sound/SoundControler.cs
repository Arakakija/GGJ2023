using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControler : Singleton<SoundControler>
{
    [SerializeField] SoundSO[] soundTracks;
    [SerializeField] SoundSO[] soundEffects;
    AudioSource audioSource;

    [SerializeField] float sfxVolume;

    private void Start()
    {
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

        for(int i = 0; i < soundTracks.Length; i++)
            if(soundTracks[i].name == name)
            {
                audioSource.clip = soundTracks[i].audio;
                audioSource.Play();
                return;
            }  
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
        AudioClip clip = null;

        for (int i = 0; i < soundEffects.Length; i++)
            if (soundEffects[i].name == name)
            {
                clip = soundEffects[i].audio;
                return;
            }

        if (clip == null) return;

        _as.clip = clip;
        _as.volume = sfxVolume;
        _as.Play();
    }
}
