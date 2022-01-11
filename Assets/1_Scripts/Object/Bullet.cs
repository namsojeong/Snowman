using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    bool isDir = false;

    Vector2 dir=Vector2.zero;

    private void Update()
    {
        if(!isDir)
        {
            dir = InGame.Instance.targetBullet;
            Debug.Log(dir);
            isDir = true;
        }
            BulletMove();
    }
    private void BulletMove()
    {
        transform.Translate(dir * 2f*Time.deltaTime);
        if(Mathf.Abs(transform.position.x)>=20f){
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject);
        }
        if(Mathf.Abs(transform.position.y) >= 20f){
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject);
        }
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
