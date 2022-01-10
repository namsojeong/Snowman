using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    bool isDir = false;

    Vector3 dir;

    private void Update()
    {
        if(!isDir)
        {
            dir = InGame.Instance.targetBullet;
            dir.z = 0f;
            isDir = true;
            BulletMove();
        }
    }
    private void BulletMove()
    {
        Debug.Log("ÀÀ");
        transform.DOMove(dir, 2f)
            .OnComplete(()=>ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="FOOT")
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, collision.gameObject);
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject);
        }
    }
}
