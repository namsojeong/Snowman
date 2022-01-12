using UnityEngine;

public class CameraMove : MonoBehaviour
{
   public Transform target; //Ÿ�� ��ġ

    Vector2 offset;
    
    float smoothSpeed = 3; //ī�޶� �̵��ӵ�
    float cameraHalfWidth, cameraHalfHeight;

    private void Start()
    {
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        CameraMoving();
    }

    //�÷��̾� �Ѿư���
    void CameraMoving()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, GameManager.Instance.limitMinX + cameraHalfWidth, GameManager.Instance.limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, GameManager.Instance.limitMinY + cameraHalfHeight, GameManager.Instance.limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }

}
