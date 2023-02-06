using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickeable : MonoBehaviour
{
    public UnityAction onClick;
    
    ParticleSystem particles;
    SpriteRenderer spriteRenderer;
    private AudioSource _as;

    [SerializeField] private string[] soundsNames;

    [SerializeField] Sprite firstSrpite;
    [SerializeField] Sprite secundSrpite;

    [SerializeField] float animationTime;
    [SerializeField] float animationTimeCounter;
    [SerializeField] float parpadeoTime;

    [SerializeField] bool animated;
    [SerializeField] bool flicker;
    [SerializeField] bool changeSrpite;

    bool canBeClicked = true;
    private bool clicked;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (gameObject.GetComponent<AudioSource>()) _as = gameObject.GetComponent<AudioSource>();
        else 
        {
            gameObject.AddComponent<AudioSource>();
            _as = gameObject.GetComponent<AudioSource>();
        }
        firstSrpite = spriteRenderer.sprite;
        animationTimeCounter = animationTime;
    }

    private void Update()
    {
        if (clicked) animationTimeCounter = (animationTimeCounter > 0) ? animationTimeCounter - Time.deltaTime : 0;
        if (clicked && animationTimeCounter <= 0)
        {
            onClick?.Invoke();
            clicked = false;
        }
    }

    public void OnClick()
    {
        if(!canBeClicked) return;
        clicked= true;
        StartAnimation();
        PlayRandSound();
    }

    void PlayRandSound()
    {
        if (soundsNames == null) return;
        if (soundsNames.Length <= 0) return;

        int rand = UnityEngine.Random.Range(0, soundsNames.Length - 1);

        SoundControler.Instance.PlaySound(soundsNames[rand], _as);
    }

        void StartAnimation()
    {
        if (animated)
        {
            if (flicker) StartCoroutine(AnimationFlicker());
            if (changeSrpite)
            {
                if (secundSrpite == null) return;
                StartCoroutine(AnimationSprite());
            }
        }
    }

    IEnumerator AnimationFlicker()
    {
        while(animationTimeCounter > 0)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(parpadeoTime);

            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(parpadeoTime);
        }
    }
    IEnumerator AnimationSprite()
    {
        while (animationTimeCounter > 0)
        {
            spriteRenderer.sprite = secundSrpite;
            yield return new WaitForSeconds(animationTimeCounter);
            spriteRenderer.sprite = firstSrpite;
        };

        if (!flicker)
        {
            animationTimeCounter = animationTime;
            canBeClicked = false;
        }
    }
}
