using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    Animator koong;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    Collider2D collider;

    Coroutine TimeC;

    bool isMoving = true; //움직이는 중인가
    bool isDamage = false; //플레이가 데미지 받는지 아닌지
    int spriteNum = 0; //빨간지 안빨간지

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        TimeC = StartCoroutine(TimeCheck());
        collider.enabled = false;
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
        collider.enabled = false;
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, InGame.Instance.player.transform.position, 0.004f); //0.005f가 플레이어 위치로 가는 속도
    }

    //고정되기 까지 시간
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(5f);
        Koong();
    }

    //쿵!
    void Koong()
    {
        StopCoroutine(TimeC);
        isMoving = false;

        gameObject.transform.localScale = new Vector3(4f, 4f, 4f);
        gameObject.transform.DOScale(new Vector3(2f, 2f, 2f), 1f)
        .OnComplete(() =>
        {
            Camera.main.DOShakePosition(0.8f);
            koong.Play("Anim");
            InvokeRepeating("KoongSprite", 0f, 1f);
        });

    }

    //쿵 했을 때 빨개지기
    void KoongSprite()
    {
        if (spriteNum == 0)
        {
            collider.enabled = true;
            isDamage = true;
            spriteRenderer.color = Color.red;
            spriteNum = 1;
        }
        else
        {
            spriteRenderer.color = Color.grey;
            collider.isTrigger = false;
            spriteNum = 0;
            InGame.Instance.SpawnFoot();
            CancelInvoke("KoongSprite");
            isDamage = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isDamage)
            {
                return;
            }
            InGame.Instance.Reset();
            SceneManager.Instance.OpenScene(2);
        }
    }

}
