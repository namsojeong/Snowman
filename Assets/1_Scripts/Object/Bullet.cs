using UnityEngine;
/// <summary>
/// 총알 움직임 구현 + 초기화
/// </summary>
public class Bullet : MonoBehaviour
{
    //방향을 정했나?
    bool isDir = false;

    //방향
    Vector2 dir = Vector2.zero;

    //총알 스피드
    float bulletSpeed = 10f;

    private void OnDisable()
    {
            isDir = false;
        gameObject.tag = "BULLET";
    }

    private void Update()
    {
        if (!isDir)
        {
        dir = InGame.Instance.targetBullet;
            isDir= true;
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

}
