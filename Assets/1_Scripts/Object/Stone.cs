using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    int time = 0;
    const int returnTime = 5;

    private void OnEnable()
    {
        InvokeRepeating("SpawnTime", 1f, 1f);
    }
    private void OnDisable()
    {
        CancelInvoke("SpawnTime");
        time = 0;
    }

    //사라질 타이밍 재기
    private void SpawnTime()
    {
        time++;
        if(time>=returnTime)
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.STONE, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            InGame.Instance.InvenStone(true);
            ObjectPool.Instance.ReturnObject(PoolObjectType.STONE, gameObject);
        }
        
    }
}
