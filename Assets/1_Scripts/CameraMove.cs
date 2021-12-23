using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject playerT;

    const int zPos = -10;

    private void Update()
    {
        CameraMoving();
    }
    private void CameraMoving()
    {

        Camera.main.transform.DOMove(new Vector3(playerT.transform.position.x, playerT.transform.position.y, zPos), 1f);
    }
}
