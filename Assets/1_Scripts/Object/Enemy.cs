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

    float enemySpeed = 0.01f; //�Ѿư��� �ӵ�
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

    //������
    private void Move()
    {
        if (!isMoving) return;
        //�÷��̾� �Ѿƴٴϱ�
        collider.enabled = false;
        isDamage = false;
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, InGame.Instance.player.transform.position, enemySpeed);
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
        isDead = false;
        spriteRenderer.color = new Color(0, 0, 0, 0.52f);
        TimeC = StartCoroutine(TimeCheck());
    }

    //��!
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

    //������Ʈ ����
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
