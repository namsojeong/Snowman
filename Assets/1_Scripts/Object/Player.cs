using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    private float movespeed = 8f; //플레이어 스피드

    public GameObject bulletObj;

    Vector3 bulletDir=Vector3.zero;

    Vector2 moveVec2=Vector2.zero;

    [SerializeField]
    VariableJoystick virtualJoystick;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        UI.Instance.UpdateSlider(InGame.Instance.snowball);
    }

    private void Update()
    {
        Move();
    }


    void Move()
    {
        if (virtualJoystick.Direction == Vector2.zero) return;
        moveVec2 = virtualJoystick.Direction;
        transform.Translate(moveVec2 * movespeed * Time.deltaTime);
        bulletDir = new Vector2(moveVec2.x, virtualJoystick.Direction.y);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        if (transform.localScale.x <= 1.6f)
        {
            transform.localScale += new Vector3(0.001f, 0.001f);
            InGame.Instance.snowball += 0.001f;
            UI.Instance.UpdateSlider(InGame.Instance.snowball);
        }

    }

    //총알 발사 버튼 
    public void OnClickFIre()
    {
        if (transform.localScale.x <= 0.6f) return;

        GameObject bullet=ObjectPool.Instance.GetObject(PoolObjectType.BULLET);
        bullet.transform.position = transform.position;
        transform.localScale -= new Vector3(0.22f, 0.22f);
        InGame.Instance.snowball -= 0.22f;
        UI.Instance.UpdateSlider(InGame.Instance.snowball);

    }

}
