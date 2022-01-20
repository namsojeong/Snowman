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

    float enemySpeed = 0.003f; //쫓아가는 속도
    float moveDelay = 3f; //쫓아다니는 시간
    float scaleDelay = 1f; //크기 바뀔 때 속도

    const float initScale = 3f; //초기 크기
    const float bigScale = 5f; //커졌을 때 크기

    bool isMoving = true; //움직이는 중인가
    bool isDamage = false; //플레이가 데미지 받는지 아닌지
    int spriteNum = 0; //빨간지 안빨간지
    int damageCount = 0;

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
        StopCoroutine(TimeC);
    }

    private void OnEnable()
    {
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
            InGame.Instance.SpawnFoot();
            Camera.main.DOShakePosition(0.8f);
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

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "BULLET")
        {
            if (InGame.Instance.haveStone)
            {
                damageCount = 0;
                EnemyReset();
                InGame.Instance.haveStone = false;
                ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, gameObject);
            }

            damageCount++;
            footPrint[damageCount - 1].SetActive(true);
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, collision.gameObject);
            if (damageCount >= 3)
            {
                damageCount = 0;
                EnemyReset();
                ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, gameObject);
            }
        }
    }

    //오브젝트 리셋
    void EnemyReset()
    {
        transform.position = InGame.Instance.player.transform.position;
        isDamage = false;
        isMoving = true;
        spriteRenderer.color = new Color(0, 0, 0, 0.52f);
        transform.localScale = new Vector2(initScale, initScale);
        collider.enabled = false;
        for(int i=0;i<3;i++)
        {
            footPrint[i].SetActive(false);
        }
    }

}
