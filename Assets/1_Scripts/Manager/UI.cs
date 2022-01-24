using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UI : MonoBehaviour
{
    public static UI Instance;

    [Header("RUNNING")]
    [SerializeField]
    Text scoreText = null;
    [SerializeField]
    Text highScoreText = null;
    [SerializeField]
    Text overText = null;
    [SerializeField]
    Slider snowbar;

    [Header("GAMEOVER")]
    [SerializeField]
    Text overHighScoreText;
    [SerializeField]
    Text overScoreText;

    [SerializeField]
    GameObject quitPanel;
    [SerializeField]
    Text plusScoreText;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("ScoreUp", 1f, 1f);
    }

    private void Update()
    {
        //백버튼
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
            Time.timeScale = 0;
        }
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
            for (float i = value; i > 0.6f; i -= 0.2f)
            {
                count++;
            }
            InGame.Instance.snowball = count;
            count = 0;
        }
    }
    
    //오브젝트 껐다켰다 + 시간 멈추기
    public void OpenObj(GameObject obj)
    {
        obj.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseObj(GameObject obj)
    {
        obj.SetActive(false);
        Time.timeScale = 1;
    }

    //스코어 올리기
    void ScoreUp()
    {
        if(!SceneM.Instance.isRunning) return;
      
        UpdateUI();
        GameManager.Instance.score++;
        if (GameManager.Instance.score > GameManager.Instance.highScore)
        {
            GameManager.Instance.highScore = GameManager.Instance.score;
        }
    }

    //인게임 스코어 UI
    void UpdateUI()
    {
        scoreText.text = string.Format($"SCORE\n{GameManager.Instance.score}");
        highScoreText.text = string.Format($"HIGHSCORE {PlayerPrefs.GetInt("HIGHSCORE",0)}");
    }

    //게임오버 UI
    public void UpdateOverUI()
    {
        overText.text = string.Format("");
        overScoreText.text = string.Format($"SCORE\n{PlayerPrefs.GetInt("SCORE", 0)}");
        overHighScoreText.text = string.Format($"HIGHSCORE {PlayerPrefs.GetInt("HIGHSCORE", 0)}");
    }

    public void OverText()
    {
        overText.DOText("GAME OVER", 2f);
    }

    //종료
    public void OnQuit()
    {
        Application.Quit();
    }

    
}
