using UnityEngine;
/// <summary>
/// �Ѿ� ������ ���� + �ʱ�ȭ
/// </summary>
public class Bullet : MonoBehaviour
{
    //������ ���߳�?
    bool isDir = false;

    //����
    Vector2 dir = Vector2.zero;

    //�Ѿ� ���ǵ�
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

    //�Ѿ� �߻�
    private void BulletMove()
    {
        transform.Translate(dir * bulletSpeed * Time.deltaTime);

        //��迵�� ������ ��
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
