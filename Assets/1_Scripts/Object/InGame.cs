using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGame : MonoBehaviour
{
    public static InGame Instance;

    public GameObject player;

    public Vector3 targetBullet = Vector3.zero;

    public float snowball = 0f;

    private void Awake()
    {
        Instance = this;
    }


    public void SpawnFoot()
    {
        GameObject foot = ObjectPool.Instance.GetObject(PoolObjectType.FOOT);
        foot.transform.position = new Vector3(player.transform.position.x - 1, player.transform.position.y - 1, 0f);
    }

    public void Reset()
    {
        ObjectPool.Instance.ResetObj();
        snowball = 0;
        player.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
        player.transform.position = new Vector3(0f, 0f, 0f);
        UI.Instance.UpdateSlider(snowball);
    }

}
