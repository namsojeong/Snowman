using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
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

    //����� Ÿ�̹� ���
    private void SpawnTime()
    {
        time++;
        if (time >= returnTime)
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.ANGEL, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.ANGEL, gameObject);
        }

    }
}
