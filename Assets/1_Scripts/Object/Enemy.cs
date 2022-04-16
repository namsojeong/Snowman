using System.Collections;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// �߿� ���� �ý��� ���� (������ + �÷��̾��� ��� �Ǵ� + �߿� ������ �Ǵ�)
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject[] footPrint;

    SpriteRenderer spriteRenderer;
    Collider2D collider;
    Coroutine TimeC;

    float enemySpeed = 1f; //�Ѿư��� �ӵ�
    private bool isDamage;
    private bool isDead;
    float moveDelay = 2.5f; //�Ѿƴٴϴ� �ð�
    float scaleDelay = 1f; //ũ�� �ٲ� �� �ӵ�

    const float initScale = 3f; //�ʱ� ũ��
    const float bigScale = 5f; //Ŀ���� �� ũ��

    bool isMoving = true; //�����̴� ���ΰ�

    int spriteNum = 0; //������ �Ȼ�����
    int damageCount = 0;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        collider.enabled = false;
    }

    private void Update()
    {
        //������ ����
        {
            Move();
        }
    }

    //Speed ������ ���̵� UP!
    void UpSpeed()
    {
        enemySpeed += 0.01f;
        if (enemySpeed >= 3f)
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
        transform.position = Vector3.Slerp(transform.position, InGame.Instance.player.transform.position, enemySpeed * Time.deltaTime);
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

        spriteRenderer.color = new Color(1, 1, 1, 1f);
        spriteRenderer.sprite = InGame.instance.footSprite[Random.Range(1, 3)];
        gameObject.transform.localScale = new Vector3(bigScale, bigScale, 1f);
        gameObject.transform.DOScale(new Vector3(initScale, initScale, 0f), scaleDelay)
        .OnComplete(() =>
        {
            float scale = (InGame.Instance.player.transform.position - transform.position).magnitude / 10;
            if (scale <= 0.4f)
            {
                scale = 0.1f;
            }
            else if (scale >= 0.8f)
            {
                scale = 10;
            }
            else if (scale >= 0.7f)
            {
                scale = 0.9f;
            }
            Camera.main.DOShakePosition(1 - scale);
            InvokeRepeating("KoongSprite", 0f, 1f);
            InGame.instance.SpawnFoot();
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

    //Player ������ �Ǵ�
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isDamage)
            {
                return;
            }
            EnemyReset();
            SceneM.Instance.SceneChange("GameOver");
        }
    }

    //������ ������ �Ǵ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead) return;
        if (collision.tag == "BULLET")
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, collision.gameObject);
            EnemyDamage();
        }
        if (collision.transform.tag == "STONESNOW")
        {
            collision.transform.tag = "BULLET";
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, collision.gameObject);
            MeetStone();
        }
    }

    //Stone �����ۿ� ����� ��
    void MeetStone()
    {
        GameManager.Instance.score += 20;
        SoundM.Instance.SoundOn("COIN", 4);
        InGame.Instance.SpawnText();
        EnemyReset();
            damageCount = 0;
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, gameObject);
    }

    //������ �Ծ��� ��
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
        spriteRenderer.sprite = InGame.instance.footSprite[0];
        spriteRenderer.color = new Color(1, 1, 1, 0.52f);
        transform.localScale = new Vector2(initScale, initScale);
        transform.position = Vector2.zero;
    }

}
