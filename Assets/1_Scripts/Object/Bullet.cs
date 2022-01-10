using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isDir = false;
    Vector3 dir;
    private void Update()
    {
        if(!isDir)
        {
            dir = InGame.Instance.dir;
            isDir = true;
        }
        transform.position -= dir * 10 * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="FOOT")
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject);
        }
    }
}
