using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    private void Update()
    {
        //플레이어 쫓아다니기
        transform.position = Vector3.Slerp(transform.position, playerTransform.position, 0.03f);

    }

}
