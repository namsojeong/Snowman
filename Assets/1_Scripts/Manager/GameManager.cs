using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float limitMinX = -27.8f, limitMaxX = 27.8f, limitMinY = -12.5f, limitMaxY = 12.5f; //영역

    public int score; //점수
    public int highScore; //최고점수

    public float playerInitScale = 0.6f; //플레이어 기본 크기
    public float moveSpeed = 8f; //플레이어 스피드

    private void Awake()
    {
        Instance = this;
    }
}
