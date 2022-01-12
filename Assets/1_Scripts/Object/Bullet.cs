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
            isDir = true;
        }
            BulletMove();
    }
    private void BulletMove()
    {
        transform.Translate(dir * 6f * Time.deltaTime);
        if(Mathf.Abs(transform.position.x)>=27.8f){
            isDir = false;
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject);
        }
        if(Mathf.Abs(transform.position.y) >= 12.5f){
            isDir = false;
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "FOOT")
        {
            isDir = false;
        }
    }
}
