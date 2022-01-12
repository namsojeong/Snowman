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

    bool isMoving = true; //�����̴� ���ΰ�
    bool isDamage = false; //�÷��̰� ������ �޴��� �ƴ���
    int spriteNum = 0; //������ �Ȼ�����

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
        transform.position = Vector3.Slerp(transform.position, InGame.Instance.player.transform.position, 0.003f); //0.005f�� �÷��̾� ��ġ�� ���� �ӵ�
    }

    //�����Ǳ� ���� �ð�
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

    //��!
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
