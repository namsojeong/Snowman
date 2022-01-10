using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGame : MonoBehaviour
{
    public static InGame Instance;

    public Transform playerTransform;

    public Vector3 dir;

    private void Awake()
    {
        Instance = this;
    }


    public void SpawnFoot()
    {
        GameObject foot = ObjectPool.Instance.GetObject(PoolObjectType.FOOT);
        foot.transform.position = new Vector3(playerTransform.position.x - 1, playerTransform.position.y - 1, 0f);
    }
}
