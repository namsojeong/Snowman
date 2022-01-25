using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    VariableJoystick virtualJoystick;

    Vector2 bulletDir = Vector2.zero;
    Vector2 moveVec2 = Vector2.zero;

    private float playerMaxScale = 1.6f; //�÷��̾� �ִ� ũ��

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        Move();
        ChangeScale();
    }
    private void OnEnable()
    {
        virtualJoystick.ResetHandle();
        
    }
    //�÷��̾� ������
    void Move()
    {
        if (virtualJoystick.Direction == Vector2.zero) return;
        moveVec2 = virtualJoystick.Direction;
        transform.Translate(moveVec2 * GameManager.Instance.moveSpeed * Time.deltaTime);
        bulletDir = new Vector2(moveVec2.x, virtualJoystick.Direction.y);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        //�÷��̾� ũ�� ����
        if (transform.localScale.x <= playerMaxScale)
        {
            InGame.Instance.PlayerScale(true);
        }
    }

    //�÷��̾� ũ�� �ٲٱ�
    void ChangeScale()
    {
        transform.localScale = new Vector3(InGame.Instance.playerScale, InGame.Instance.playerScale, 1f);
        UI.Instance.UpdateSlider(InGame.Instance.playerScale);
    }

    //�Ѿ� �߻� ��ư 
    public void OnClickFIre()
    {
        if (InGame.Instance.snowball < 1) return;
        InGame.Instance.SnowBall();
        GameObject bullet;
        bullet = ObjectPool.Instance.GetObject(PoolObjectType.BULLET);
        bullet.transform.position = transform.position;
        InGame.Instance.PlayerScale(false);
        if(InGame.Instance.haveStone)
        {
        SoundM.Instance.SoundOn("SFX",3);
            bullet.tag = "STONESNOW";
            InGame.Instance.InvenStone(false);
        }
        else
        {
        SoundM.Instance.SoundOn("SFX", 2);
        }
    }

}
