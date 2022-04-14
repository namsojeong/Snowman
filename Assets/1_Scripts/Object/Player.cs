using UnityEngine;
/// <summary>
/// 플레이어에 관한 스크립트
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField]
    VariableJoystick virtualJoystick;

    Vector2 bulletDir = Vector2.zero;
    Vector2 moveVec2 = Vector2.zero;

    private float playerMaxScale = 1.6f; //플레이어 최대 크기

    private void Start()
    {
        virtualJoystick.ResetHandle();
    }

    private void Update()
    {
        Move();
        ChangeScale();
    }

    //플레이어 움직임
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

        //플레이어 크기 증가
        if (transform.localScale.x <= playerMaxScale)
        {
            InGame.Instance.PlayerScale(true);
        }
    }

    //플레이어 크기 바꾸기
    void ChangeScale()
    {
        transform.localScale = new Vector3(InGame.Instance.playerScale, InGame.Instance.playerScale, 1f);
        InGameUI.Instance.UpdateSlider(InGame.Instance.playerScale);
    }
}
