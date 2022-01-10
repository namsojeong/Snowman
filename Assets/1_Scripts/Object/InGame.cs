using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public static InGame Instance;
    
    public Transform playerTransform;

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
