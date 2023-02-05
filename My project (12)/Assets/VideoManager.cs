using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : Singleton<VideoManager>
{
    public RawImage image;
    public VideoPlayer player;
    public AudioSource source;

    public float _speed;

    private void Start()
    {
        StartCoroutine(FadeImage(true));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        yield return new WaitUntil(() => player.clockTime >= 40);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * _speed)
            {
                // set color with i as alpha
                image.color =  new Color(1, 1, 1, i);
                source.volume = i;
                yield return null;
            }
        }
    }
}
