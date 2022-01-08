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
    SpriteRenderer footSpriteRenderer;
    [SerializeField]
    GameObject foot;

    Coroutine TimeC;

    bool isMoving = true; //�����̴� ���ΰ�
    
    int spriteNum = 0; //������ �Ȼ�����

    private void Start()
    {
        foot.SetActive(false);
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
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, playerTransform.position, 0.001f); //0.005f�� �÷��̾� ��ġ�� ���� �ӵ�
    }

    //�����Ǳ� ���� �ð�
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(5f);
        foot.SetActive(true);
        Koong();
    }

    //��!
    void Koong()
    {
        StopCoroutine(TimeC);
        isMoving = false;
        foot.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(()=>
        {
            Camera.main.DOShakePosition(0.8f);
            koong.Play("Anim");
            InvokeRepeating("KoongSprite", 0f, 2f);
            
        });
        
    }

    //�� ���� �� ��������
    void KoongSprite()
    {
        if (spriteNum == 0)
        {
            footSpriteRenderer.color = Color.red;
            spriteNum = 1;
        }
        else
        {
            footSpriteRenderer.color = Color.grey;
            spriteNum = 0;
            CancelInvoke("KoongSprite");
        }
    }
}
