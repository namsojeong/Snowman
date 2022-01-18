using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    int time = 0;
    private void OnEnable()
    {
        InvokeRepeating("TimeCheck", 1f, 1f);
    }

    private void OnDisable()
    {
        CancelInvoke("TimeCheck");
    }

    void TimeCheck()
    {
        time++;
        if(time>=10)
        {
            time = 0;
            CancelInvoke("TimeCheck");
            ObjectPool.Instance.ReturnObject(PoolObjectType.LIGHT, gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.moveSpeed = 4f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.moveSpeed = 8f;
            InGame.Instance.snowball -= 1;
            if (InGame.Instance.snowball <= 0)
                InGame.Instance.snowball = 0;
        }
        
    }
}
