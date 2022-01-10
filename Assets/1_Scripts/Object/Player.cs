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

    //조이스틱 움직임
    //void Move()
    //{
    //    float x=1f;
    //    float y=1f;

    //    Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
    //    viewPos.x = Mathf.Clamp01(viewPos.x);
    //    viewPos.y = Mathf.Clamp01(viewPos.y);
    //    if (viewPos.x < 0f) viewPos.x = 0f;
    //    if (viewPos.y < 0f) viewPos.y = 0f;
    //    if (viewPos.x > 1f) viewPos.x = 1f;
    //    if (viewPos.y > 1f) viewPos.y = 1f;
    //    Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
    //    transform.position = worldPos;
    //    if (virtualJoystick.Direction == Vector2.zero) return;
    //    x = virtualJoystick.Horizontal;
    //    y = virtualJoystick.Vertical;
    //        Debug.Log(x);
    //        Debug.Log(y);
    //        transform.position += new Vector3(x, y, 0) * movespeed * Time.deltaTime;
    //        bulletDir = transform.position + new Vector3(x *1.2f, y * 1.2f, 0);
    //        if (transform.localScale.x <= 1.5f)
    //        {
    //            transform.localScale += new Vector3(0.001f, 0.001f);
    //            InGame.Instance.snowball += 0.001f;
    //            UI.Instance.UpdateSlider(InGame.Instance.snowball);
    //        }

    //}

    //발사버튼

    void Move()
    {
        if (virtualJoystick.Direction == Vector2.zero) return;
        moveVec2 = virtualJoystick.Direction;
        transform.Translate(moveVec2 * movespeed * Time.deltaTime);
        bulletDir = new Vector2(moveVec2.x, virtualJoystick.Direction.y);
        if (transform.localScale.x <= 1.5f)
        {
            transform.localScale += new Vector3(0.001f, 0.001f);
            InGame.Instance.snowball += 0.001f;
            UI.Instance.UpdateSlider(InGame.Instance.snowball);
        }

    }
    public void OnClickFIre()
    {
        if (transform.localScale.x <= 0.6f) return;

        GameObject bullet=ObjectPool.Instance.GetObject(PoolObjectType.BULLET);
        bullet.transform.position = transform.position;
        float rotationZ = Mathf.Atan2(moveVec2.y, moveVec2.x) * Mathf.Rad2Deg;
        if(moveVec2.x<=0)
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ+180);
        else
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        InGame.Instance.targetBullet = moveVec2;
        transform.localScale -= new Vector3(0.012f, 0.012f);
        InGame.Instance.snowball -= 0.012f;
        UI.Instance.UpdateSlider(InGame.Instance.snowball);

    }

}
