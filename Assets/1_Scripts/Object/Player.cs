using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    [SerializeField]
    private VirtualJoystick virtualJoystick;
    [SerializeField]
    private float movespeed = 12f;
    [SerializeField]
    float bulletSpeed = 1;

    public GameObject bulletObj;

    int snowball = 0;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

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
            }

        }
    }

    //발사버튼
    public void OnClickFIre()
    {
        transform.localScale -= new Vector3(0.003f, 0.003f);
    }

    //void Fire()
    //{
    //    Instantiate(bulleObj, transform.position, transform.rotation);
    //    Destroy(gameObject, 5);
    //}

}
