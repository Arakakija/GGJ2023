using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickeable : MonoBehaviour
{
    public UnityAction onClick;
    
    ParticleSystem particles;
    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite firstSrpite;
    [SerializeField] Sprite secundSrpite;

    [SerializeField] float animationTime;
    [SerializeField] float animationTimeCounter;
    [SerializeField] float parpadeoTime;

    [SerializeField] bool animated;
    [SerializeField] bool flicker;
    [SerializeField] bool changeSrpite;

    bool clicked;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        firstSrpite = spriteRenderer.sprite;
        animationTimeCounter = animationTime;
    }

    private void Update()
    {
        if (clicked) animationTimeCounter = (animationTimeCounter > 0) ? animationTimeCounter - Time.deltaTime : 0;
    }

    public void OnClick()
    {
        onClick?.Invoke();
        clicked = true;

        StartAnimation();
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
            clicked = false;
        }
    }
}
