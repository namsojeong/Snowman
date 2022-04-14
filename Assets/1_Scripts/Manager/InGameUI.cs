using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 인게임 속 UI 구현
/// </summary>
public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance;

    [Header("RUNNING")]
    [SerializeField]
    Text scoreText = null;
    [SerializeField]
    Text highScoreText = null;
    [SerializeField]
    Slider snowbar;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //점수 추가 및 인게임 UI 업데이트
        {
            InvokeRepeating("ScoreUp", 1f, 1f);
        }
    }

    //스코어 올리기
    void ScoreUp()
    {
        //게임 플레이 정보 저장
        GameManager.Instance.score++;
        PlayerPrefs.SetInt("SCORE", GameManager.Instance.score);
        Debug.Log(PlayerPrefs.GetInt("SCORE"));
        if (GameManager.Instance.score > GameManager.Instance.highScore)
        {
            GameManager.Instance.highScore = GameManager.Instance.score;
            Debug.Log(PlayerPrefs.GetInt("HIGHSCORE"));
            PlayerPrefs.SetInt("HIGHSCORE", GameManager.Instance.score);
        }
        UpdateUI();
    }

    //게이지 바 UI + 스노우볼 개수
    public void UpdateSlider(float value)
    {
        snowbar.value = value;

        if (snowbar.value <= 0)
        {
            snowbar.value = 0;
            InGame.Instance.snowball = 0;
        }
        else
        {
            int count = 0;
            if (snowbar.value < 0.8f)
            {
                InGame.Instance.snowball = 0;
                return;
            }
            for (float i = value; i > 0.6f; i -= 0.2f)
            {
                count++;
            }
            InGame.Instance.snowball = count;
            count = 0;
        }
    }

    //인게임 스코어 UI
    public void UpdateUI()
    {
        scoreText.text = string.Format($"{GameManager.Instance.score}");
        highScoreText.text = string.Format($"BEST {PlayerPrefs.GetInt("HIGHSCORE", 0)}");
    }

}
