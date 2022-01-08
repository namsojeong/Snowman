using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    

    Coroutine TimeC;

    bool isMoving = true;

    private void Start()
    {
        TimeC=StartCoroutine(TimeCheck());
        
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (!isMoving) return;
        //플레이어 쫓아다니기
        isMoving = true;
        transform.position = Vector3.Slerp(transform.position, playerTransform.position, 0.005f); //0.005f가 플레이어 위치로 가는 속도
    }
    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(5f);
        isMoving = false;
        Camera.main.DOShakePosition(0.8f);
        StopCoroutine(TimeC);
    }
}
