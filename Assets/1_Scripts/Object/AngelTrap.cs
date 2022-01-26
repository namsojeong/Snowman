using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FOOT")
        {
            SoundM.Instance.SoundOn("COIN", 4);
            ObjectPool.Instance.PlusScoreUI();
            GameManager.Instance.score += GameManager.Instance.plusScore;
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, collision.gameObject);
            ObjectPool.Instance.ReturnObject(PoolObjectType.ANGELITEM, gameObject);
        }
    }
}
