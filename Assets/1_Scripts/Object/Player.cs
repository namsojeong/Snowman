using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    [SerializeField]
    private VirtualJoystick virtualJoystick;
    [SerializeField]
    private float movespeed = 8f; //플레이어 스피드
    [SerializeField]
    float bulletSpeed = 1;

    public GameObject bulletObj;

    float snowball = 0f;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        UI.Instance.UpdateSlider(snowball);
        // GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    private void Update()
    {
        Move();
        //Fire();
    }

    //조이스틱 움직임
    void Move()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        if (viewPos.x < 0f) viewPos.x = 0f;
        if (viewPos.y < 0f) viewPos.y = 0f;
        if (viewPos.x > 1f) viewPos.x = 1f;
        if (viewPos.y > 1f) viewPos.y = 1f;
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos;
        float x = virtualJoystick.Horizontal();
        float y = virtualJoystick.Vertical();
        if (x != 0 || y != 0)
        {
            transform.position += new Vector3(x, y, 0) * movespeed * Time.deltaTime;
            if (transform.localScale.x <= 1.5f)
            {
                transform.localScale += new Vector3(0.001f, 0.001f);
                snowball += 0.001f;
                UI.Instance.UpdateSlider(snowball);
            }

        }
    }

    //발사버튼
    public void OnClickFIre()
    {
        if (transform.localScale.x <= 0.6f)
            return;
            transform.localScale -= new Vector3(0.012f, 0.012f);
        snowball -= 0.012f;
        UI.Instance.UpdateSlider(snowball);
    }

    //void Fire()
    //{
    //    Instantiate(bulleObj, transform.position, transform.rotation);
    //    Destroy(gameObject, 5);
    //}


  
}
