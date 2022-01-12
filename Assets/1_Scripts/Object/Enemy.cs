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
        
        transform.position = Vector2.zero;
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
        isDamage = false;
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, InGame.Instance.player.transform.position, 0.003f); //0.005f가 플레이어 위치로 가는 속도
    }

    //고정되기 까지 시간
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(3f);
        Koong();

    }

    private void OnDisable()
    {
        StopCoroutine(TimeC);
       // EnemyReset();
        
    }

    private void OnEnable()
    {
        TimeC = StartCoroutine(TimeCheck());
    }

    //쿵!
    void Koong()
    {
        isMoving = false;

        gameObject.transform.localScale = new Vector3(5f, 5f, 1f);
        gameObject.transform.DOScale(new Vector3(2f, 2f, 2f), 1f)
        .OnComplete(() =>
        {
            InGame.Instance.SpawnFoot();
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
            spriteRenderer.color = Color.red;
            collider.enabled = true;
            isDamage = true;
            spriteNum = 1;
        }
        else
        {
            spriteRenderer.color = Color.black;
            spriteNum = 0;
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
            EnemyReset();
            SceneManager.Instance.OpenScene(2);
            
        }
        if (collision.transform.tag == "BULLET")
        {
            EnemyReset();
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, collision.gameObject);
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, gameObject);
        }

       
    }

    void EnemyReset()
    {
        transform.position = InGame.Instance.player.transform.position;
        isDamage = false;
        isMoving = true;
        spriteRenderer.color = new Color(0, 0, 0, 0.52f);
        transform.localScale = new Vector3(3f, 3f, 1f);
        collider.enabled = false;
    }
    
}
