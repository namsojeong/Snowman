using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    [SerializeField]
    private VirtualJoystick virtualJoystick;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    [SerializeField ]
    private float movespeed = 12f;

    private void Update()
    {
        //���̽�ƽ
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos;
        float x = virtualJoystick.Horizontal();
        float y = virtualJoystick.Vertical();
        if (x != 0 || y != 0)
        {
            transform.position += new Vector3(x, y, 0) * movespeed * Time.deltaTime;
        }
    }
}
