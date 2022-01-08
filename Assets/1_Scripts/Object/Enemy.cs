using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Animator koong;

    SpriteRenderer spriteRenderer;

    Coroutine TimeC;

    bool isMoving = true; //움직이는 중인가
    int spriteNum = 0; //빨간지 안빨간지

    private void Start()
    {
        TimeC = StartCoroutine(TimeCheck());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Move();
    }

    //움직임
    private void Move()
    {
        if (!isMoving) return;
        //플레이어 쫓아다니기
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, playerTransform.position, 0.005f); //0.005f가 플레이어 위치로 가는 속도
    }

    //고정되기 까지 시간
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(5f);
        InvokeRepeating("KoongSprite", 0f, 2f);
        Koong();
    }

    //쿵!
    void Koong()
    {
        StopCoroutine(TimeC);
        isMoving = false;
        Camera.main.DOShakePosition(0.8f);
        koong.Play("Anim");
    }

    //쿵 했을 때 빨개지기
    void KoongSprite()
    {
        if (spriteNum == 0)
        {
            spriteRenderer.color = Color.red;
            spriteNum = 1;
        }
        else
        {
            spriteRenderer.color = Color.black;
            spriteNum = 0;
        }
    }
}
