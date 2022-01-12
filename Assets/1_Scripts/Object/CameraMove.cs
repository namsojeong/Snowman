using UnityEngine;

public class CameraMove : MonoBehaviour
{
   public Transform target; //타겟 위치

    Vector2 offset;
    
    float smoothSpeed = 3; //카메라 이동속도
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

    //플레이어 쫓아가기
    void CameraMoving()
    {
        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, GameManager.Instance.limitMinX + cameraHalfWidth, GameManager.Instance.limitMaxX - cameraHalfWidth),   // X
            Mathf.Clamp(target.position.y + offset.y, GameManager.Instance.limitMinY + cameraHalfHeight, GameManager.Instance.limitMaxY - cameraHalfHeight), // Y
            -10);                                                                                                  // Z
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
    }

}
