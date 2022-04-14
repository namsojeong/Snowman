using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 눈천사 아이템 사용 후 (수정필요)
/// </summary>
public class AngelTrap : MonoBehaviour
{

    //발에 닿았을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FOOT")
        {
            SoundM.Instance.SoundOn("COIN", 4);
            InGame.Instance.SpawnText();
            GameManager.Instance.score += GameManager.Instance.plusScore;
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, collision.gameObject);
            ObjectPool.Instance.ReturnObject(PoolObjectType.ANGELITEM, gameObject);
        }
    }
}
