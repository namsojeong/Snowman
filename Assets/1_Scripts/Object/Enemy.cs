using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject[] footPrint;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    Collider2D collider;

    Coroutine TimeC;

    float enemySpeed = 0.01f; //쫓아가는 속도
    float moveDelay = 2.5f; //쫓아다니는 시간
    float scaleDelay = 1f; //크기 바뀔 때 속도

    const float initScale = 3f; //초기 크기
    const float bigScale = 5f; //커졌을 때 크기

    bool isMoving = true; //움직이는 중인가
    bool isDamage = false; //플레이가 데미지 받는지 아닌지
    bool isDead = false;
    int spriteNum = 0; //빨간지 안빨간지
    int damageCount = 0;

    private void Start()
    {
        collider = GetComponent<Collider2D>();

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
        transform.position = Vector3.Slerp(transform.position, InGame.Instance.player.transform.position, enemySpeed);
    }

    //고정될 때까지의  시간
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(moveDelay);
        Koong();
    }

    //시작과 끝의 코루틴
    private void OnDisable()
    {
        EnemyReset();
        StopCoroutine(TimeC);
    }

    private void OnEnable()
    {
        isDead = false;
        spriteRenderer.color = new Color(0, 0, 0, 0.52f);
        TimeC = StartCoroutine(TimeCheck());
    }

    //쿵!
    void Koong()
    {
        isMoving = false;

        gameObject.transform.localScale = new Vector3(bigScale, bigScale, 1f);
        gameObject.transform.DOScale(new Vector3(initScale, initScale, 0f), scaleDelay)
        .OnComplete(() =>
        {
            Camera.main.DOShakePosition(0.8f);
            InvokeRepeating("KoongSprite", 0f, 1f);
        });

    }

    //쿵 했을 때 빨개지기
    void KoongSprite()
    {
        damageCount = 0;
        SoundM.Instance.SoundOn("SFX", 1);
        if (spriteNum == 0)
        {
            isDead = false;
            spriteRenderer.color = Color.red;
            collider.enabled = true;
            spriteNum = 1;
            isDamage = true;
        }
        else
        {
            spriteNum = 0;
            CancelInvoke("KoongSprite");
            isDead = true;
            isDamage = false;
            spriteRenderer.color = Color.black;
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
            SceneM.Instance.OpenScene(2);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead) return;
        if (collision.transform.tag == "BULLET")
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, collision.gameObject);
            EnemyDamage();
        }
        if (collision.transform.tag == "STONESNOW")
        {
            GameManager.Instance.score += 20;
            SoundM.Instance.SoundOn("COIN", 4);
            //UI.Instance.EffectText();
            collider.tag = "BULLET";
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, collision.gameObject);
            damageCount = 0;
            EnemyReset();
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, gameObject);
        }
    }

    void EnemyDamage()
    {
        damageCount++;
        footPrint[damageCount - 1].SetActive(true);
        if (damageCount >= 3)
        {
            SoundM.Instance.SoundOn("COIN", 4);
            //UI.Instance.EffectText();
            GameManager.Instance.score += 20;
            damageCount = 0;
            EnemyReset();
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, gameObject);
        }
    }

    //오브젝트 리셋
    void EnemyReset()
    {
        for (int i = 0; i < 3; i++)
        {
            footPrint[i].SetActive(false);
        }
        isDamage = false;
        isDead = false;
        isMoving = true;
        spriteRenderer.color = new Color(0, 0, 0, 0.52f);
        transform.localScale = new Vector2(initScale, initScale);
        transform.position = Vector2.zero;
    }

}
