using UnityEngine;
/// <summary>
/// 인게임 수치들
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance != null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public float limitMinX = -27.8f, limitMaxX = 27.8f, limitMinY = -12.5f, limitMaxY = 12.5f; //영역

    public int score; //점수
    public int highScore; //최고점수
    public int plusScore=20; //추가점수

    public float playerInitScale = 0.6f; //플레이어 기본 크기
    public float moveSpeed = 8f; //플레이어 스피드

    private void Awake()
    {
        instance = this;

        //점수 세팅 및 초기화
        {
            PlayerPrefs.SetInt("SCORE", 0);
            score = 0;
            highScore = PlayerPrefs.GetInt("HIGHSCORE", 0);
        }

    }
}
