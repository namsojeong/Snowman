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

    SpriteRenderer spriteRenderer;

    Coroutine TimeC;

    bool isMoving = true; //�����̴� ���ΰ�
    int spriteNum = 0; //������ �Ȼ�����

    private void Start()
    {
        TimeC = StartCoroutine(TimeCheck());
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, playerTransform.position, 0.005f); //0.005f�� �÷��̾� ��ġ�� ���� �ӵ�
    }

    //�����Ǳ� ���� �ð�
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(5f);
        InvokeRepeating("KoongSprite", 0f, 2f);
        Koong();
    }

    //��!
    void Koong()
    {
        StopCoroutine(TimeC);
        isMoving = false;
        Camera.main.DOShakePosition(0.8f);
        koong.Play("Anim");
    }

    //�� ���� �� ��������
    void KoongSprite()
    {
        if (spriteNum == 0)
        {
            spriteRenderer.color = Color.red;
            spriteNum = 1;
        }
        else
        {
            spriteRenderer.color = Color.black;
            spriteNum = 0;
        }
    }
}
