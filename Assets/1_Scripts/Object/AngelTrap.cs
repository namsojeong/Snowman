using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FOOT")
        {
            GameManager.Instance.score += GameManager.Instance.plusScore;
            //UI.Instance.EffectText();
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, collision.gameObject);
            ObjectPool.Instance.ReturnObject(PoolObjectType.ANGELITEM, gameObject);
        }
    }
}
