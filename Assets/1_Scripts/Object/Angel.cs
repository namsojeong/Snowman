using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    int time = 0;
    const int returnTime = 10;


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
        if (time >= returnTime)
        {
            ObjectPool.Instance.ReturnObject(PoolObjectType.ANGEL, gameObject);
        }
    }

    //아이템 획득
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (InGame.Instance.haveAngel) return;
            SoundM.Instance.SoundOn("SFX", 5);
            InGame.Instance.InvenOn("ANGEL", true);
            ObjectPool.Instance.ReturnObject(PoolObjectType.ANGEL, gameObject);
        }
    }
}
