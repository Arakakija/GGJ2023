using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class SoundControler : Singleton<SoundControler>
{
    [SerializeField] SoundSO[] soundTracks;
    [SerializeField] SoundSO[] soundEffects;
    AudioSource audioSource;

    List<AudioSource> sfxSources= new List<AudioSource>();

    [SerializeField] float sfxVolume;

    private void Start()
    {
        if (gameObject.GetComponent<AudioSource>()) audioSource = gameObject.GetComponent<AudioSource>();
    }

    AudioSource AddNewSource()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        sfxSources.Add(audio);
        return audio;
    }

    AudioSource UseSource()
    {
        for (int i = 0;i < sfxSources.Count; i++) 
        {
            if (sfxSources[i].isPlaying == false)
                return sfxSources[i];
        }
        return AddNewSource();
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

    public void PlaySound(string name)
    {
        AudioSource audio = UseSource();
        for (int i = 0; i < soundEffects.Length; i++)
            if (soundEffects[i].name == name)
            {
                audio.clip = soundEffects[i].audio;
                audio.volume = sfxVolume;
                audio.Play();
                return;
            }
    }
}
