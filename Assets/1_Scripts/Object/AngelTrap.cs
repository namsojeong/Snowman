using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��õ�� ������ ��� �� (�����ʿ�)
/// </summary>
public class AngelTrap : MonoBehaviour
{

    //�߿� ����� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FOOT")
        {
            MeetFoot();
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "FOOT")
        {
            MeetFoot();
            ObjectPool.Instance.ReturnObject(PoolObjectType.FOOT, collision.gameObject);
        }
    }

    void MeetFoot()
    {
        SoundM.Instance.SoundOn("COIN", 4);
        InGame.Instance.SpawnText();
        GameManager.Instance.score += GameManager.Instance.plusScore;
        ObjectPool.Instance.ReturnObject(PoolObjectType.ANGELITEM, gameObject);
    }
}
