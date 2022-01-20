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

    float enemySpeed = 0.003f; //�Ѿư��� �ӵ�
    float moveDelay = 3f; //�Ѿƴٴϴ� �ð�
    float scaleDelay = 1f; //ũ�� �ٲ� �� �ӵ�

    const float initScale = 3f; //�ʱ� ũ��
    const float bigScale = 5f; //Ŀ���� �� ũ��

    bool isMoving = true; //�����̴� ���ΰ�
    bool isDamage = false; //�÷��̰� ������ �޴��� �ƴ���
    int spriteNum = 0; //������ �Ȼ�����
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
        StopCoroutine(TimeC);
    }

    private void OnEnable()
    {
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
            InGame.Instance.SpawnFoot();
            Camera.main.DOShakePosition(0.8f);
            InvokeRepeating("KoongSprite", 0f, 1f);
        });

    }

    //�� ���� �� ��������
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

    //������Ʈ ����
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
