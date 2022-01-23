using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tear : MonoBehaviour
{
    bool isDis = true;
    int delayTime = 0;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        isDis = false;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        InvokeRepeating("Delay", 1f, 1f);
    }
    void MoveTear()
    {
        transform.localPosition = new Vector2(6.53f, 1.38f);
        transform.DOMove(new Vector3(6.53f, -2f), 2.5f)
            .OnComplete(() =>
            {
                StartTear();
            });
    }
    void Delay()
    {
        delayTime++;
        if(delayTime>=3)
        {
            delayTime = 0; 
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            StartTear();
            CancelInvoke("Delay");
        }
    }
    void StartTear()
    {
        if (isDis) return;
        MoveTear();
    }
    private void OnDisable()
    {
        transform.DOKill();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        transform.localPosition = new Vector2(6.53f, 1.38f);
        isDis = true;
    }
}
