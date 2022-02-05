using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject[] footPrint;
    [SerializeField]
    Sprite[] footSprite;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    Collider2D collider;

    Coroutine TimeC;

    float enemySpeed = 1f; //�Ѿư��� �ӵ�
    float moveDelay = 2.5f; //�Ѿƴٴϴ� �ð�
    float scaleDelay = 1f; //ũ�� �ٲ� �� �ӵ�

    const float initScale = 3f; //�ʱ� ũ��
    const float bigScale = 5f; //Ŀ���� �� ũ��

    bool isMoving = true; //�����̴� ���ΰ�
    bool isDamage = false; //�÷��̰� ������ �޴��� �ƴ���
    bool isDead = false;
    int spriteNum = 0; //������ �Ȼ�����
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
    void UpSpeed()
    {
        enemySpeed += 0.01f;
        if(enemySpeed>=3f)
        {
            enemySpeed = 3f;
            CancelInvoke("UpSpeed");
        }
    }

    //������
    private void Move()
    {
        if (!isMoving) return;
        //�÷��̾� �Ѿƴٴϱ�
        collider.enabled = false;
        isDamage = false;
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, InGame.Instance.player.transform.position, enemySpeed*Time.deltaTime);
    }

    //������ ��������  �ð�
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(moveDelay);
        Koong();
    }

    //���۰� ���� �ڷ�ƾ
    private void OnDisable()
    {
        EnemyReset();
        StopCoroutine(TimeC);
    }

    private void OnEnable()
    {
        TimeC = StartCoroutine(TimeCheck());
        InvokeRepeating("UpSpeed", 1f, 0.3f);
    }

    //��!
    void Koong()
    {
        isMoving = false;

        spriteRenderer.color = new Color(1, 1,1 , 1f);
        spriteRenderer.sprite = footSprite[Random.Range(1, 3)];
        gameObject.transform.localScale = new Vector3(bigScale, bigScale, 1f);
        gameObject.transform.DOScale(new Vector3(initScale, initScale, 0f), scaleDelay)
        .OnComplete(() =>
        {
            float scale = (InGame.Instance.player.transform.position - transform.position).magnitude / 10;
            if (scale <= 0.4f)
            {
                scale = 0.1f;
            }
            else if(scale>=0.8f)
            {
                scale = 10;
            }
            else if (scale >= 0.7f)
            {
                scale = 0.9f;
            }
            Camera.main.DOShakePosition(1 - scale);
            InvokeRepeating("KoongSprite", 0f, 1f);
        });

    }

    //�� ���� �� ��������
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
            spriteRenderer.color = Color.white;
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
            InGame.Instance.SpawnText();
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
            InGame.Instance.SpawnText();
            GameManager.Instance.score += 20;
            damageCount = 0;
            EnemyReset();
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, gameObject);
        }
    }

    //������Ʈ ����
    void EnemyReset()
    {
        for (int i = 0; i < 3; i++)
        {
            footPrint[i].SetActive(false);
        }
        enemySpeed = 1f;
        isDamage = false;
        isDead = false;
        isMoving = true;
        spriteRenderer.sprite = footSprite[0];
        spriteRenderer.color = new Color(1, 1,1 , 0.52f);
        transform.localScale = new Vector2(initScale, initScale);
        transform.position = Vector2.zero;
    }

}
