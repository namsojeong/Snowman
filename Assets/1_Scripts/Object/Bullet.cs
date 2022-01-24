using UnityEngine;

public class Bullet : MonoBehaviour
{
    //방향을 정했나?
    bool isDir = false;

    //방향
    Vector2 dir = Vector2.zero;

    //총알 스피드
    float bulletSpeed = 5f;

    private void OnDisable()
    {
        gameObject.tag = "BULLET";
    }
    private void Update()
    {
        //방향 정해주기
        if (!isDir)
        {
            dir = InGame.Instance.targetBullet;
            isDir = true;
        }

        BulletMove();
    }

    //총알 발사
    private void BulletMove()
    {
        transform.Translate(dir * bulletSpeed * Time.deltaTime);

        //경계영역 나갔을 때
        if (Mathf.Abs(transform.position.x) >= GameManager.Instance.limitMaxX)
        {
            isDir = false;
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject);
        }
        if (Mathf.Abs(transform.position.y) >= GameManager.Instance.limitMaxY)
        {
            isDir = false;
            ObjectPool.Instance.ReturnObject(PoolObjectType.BULLET, gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //발에 충돌시
        if (collision.transform.tag == "FOOT")
        {
            isDir = false;
        }

    }

}
