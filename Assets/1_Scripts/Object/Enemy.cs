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
    SpriteRenderer spriteRenderer;
    Collider2D collider;

    Coroutine TimeC;

    bool isMoving = true; //�����̴� ���ΰ�
    bool isDamage = false; //�÷��̰� ������ �޴��� �ƴ���
    int spriteNum = 0; //������ �Ȼ�����

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        TimeC = StartCoroutine(TimeCheck());

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
        collider.isTrigger = true;
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, playerTransform.position, 0.004f); //0.005f�� �÷��̾� ��ġ�� ���� �ӵ�
    }

    //�����Ǳ� ���� �ð�
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(5f);
        Koong();
    }

    //��!
    void Koong()
    {
        StopCoroutine(TimeC);
        isMoving = false;

        gameObject.transform.localScale = new Vector3(4f, 4f, 4f);
        gameObject.transform.DOScale(new Vector3(2f, 2f, 2f), 1f)
            .OnComplete(() =>
        {
            isDamage = true;
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
            isDamage = true;
            spriteRenderer.color = Color.red;
            spriteNum = 1;
        }
        else
        {
            isDamage = false;
            spriteRenderer.color = Color.grey;
            spriteNum = 0;
            collider.isTrigger = false;
            CancelInvoke("KoongSprite");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isDamage)
            {
                return;
            }
            SceneManager.Instance.OpenScene(2);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isDamage)
            {
                return;
            }
            SceneManager.Instance.OpenScene(2);

        }
    }
}
