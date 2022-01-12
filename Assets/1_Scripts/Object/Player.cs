using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    VariableJoystick virtualJoystick;

    Vector2 bulletDir = Vector2.zero;
    Vector2 moveVec2 = Vector2.zero;

    private float movespeed = 8f; //�÷��̾� ���ǵ�
    private float playerMaxScale = 1.6f; //�÷��̾� �ִ� ũ��
    private float playerPlusScale = 0.001f; //�÷��̾� ������ �� �ʴ� �����ϴ� ũ��
    private float playerMinusScale = 0.2f; //�÷��̾� �߻��� �� �����ϴ� ũ��

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        UI.Instance.UpdateSlider(InGame.Instance.snowball);
    }

    private void Update()
    {
        Move();
    }

    //�÷��̾� ������
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

        //�÷��̾� ũ�� ����
        if (transform.localScale.x <= playerMaxScale)
        {
            transform.localScale += new Vector3(playerPlusScale, playerPlusScale);
            InGame.Instance.snowball += playerPlusScale;
            UI.Instance.UpdateSlider(InGame.Instance.snowball);
        }

    }

    //�Ѿ� �߻� ��ư 
    public void OnClickFIre()
    {
        if (transform.localScale.x <= GameManager.Instance.playerInitScale) return;

        GameObject bullet = ObjectPool.Instance.GetObject(PoolObjectType.BULLET);
        bullet.transform.position = transform.position;
        transform.localScale -= new Vector3(playerMinusScale, playerMinusScale);
        InGame.Instance.snowball -= playerMinusScale;
        UI.Instance.UpdateSlider(InGame.Instance.snowball);

    }

}
