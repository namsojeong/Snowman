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
        //���ư
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //������ �� UI + ����캼 ����
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
    
    //������Ʈ �����״� + �ð� ���߱�
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

    //���ھ� �ø���
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

    //�ΰ��� ���ھ� UI
    void UpdateUI()
    {
        scoreText.text = string.Format($"SCORE\n{GameManager.Instance.score}");
        highScoreText.text = string.Format($"HIGHSCORE {PlayerPrefs.GetInt("HIGHSCORE",0)}");
    }

    //���ӿ��� UI
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

    //����
    public void OnQuit()
    {
        Application.Quit();
    }

    
}
