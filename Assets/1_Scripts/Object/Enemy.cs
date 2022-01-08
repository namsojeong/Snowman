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
    [SerializeField]
    SpriteRenderer footSpriteRenderer;
    [SerializeField]
    GameObject foot;

    Coroutine TimeC;

    bool isMoving = true; //움직이는 중인가
    
    int spriteNum = 0; //빨간지 안빨간지

    private void Start()
    {
        foot.SetActive(false);
        TimeC = StartCoroutine(TimeCheck());
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
        transform.position = Vector3.Slerp(transform.position, playerTransform.position, 0.001f); //0.005f가 플레이어 위치로 가는 속도
    }

    //고정되기 까지 시간
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(5f);
        foot.SetActive(true);
        Koong();
    }

    //쿵!
    void Koong()
    {
        StopCoroutine(TimeC);
        isMoving = false;
        foot.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(()=>
        {
            Camera.main.DOShakePosition(0.8f);
            koong.Play("Anim");
            InvokeRepeating("KoongSprite", 0f, 2f);
            
        });
        
    }

    //쿵 했을 때 빨개지기
    void KoongSprite()
    {
        if (spriteNum == 0)
        {
            footSpriteRenderer.color = Color.red;
            spriteNum = 1;
        }
        else
        {
            footSpriteRenderer.color = Color.grey;
            spriteNum = 0;
            CancelInvoke("KoongSprite");
        }
    }
}
