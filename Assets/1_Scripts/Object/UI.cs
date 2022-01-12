using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField]
    Text scoreText = null;

    [SerializeField]
    Text highScoreText = null;

    [SerializeField]
    Slider snowbar;

    [SerializeField]
    Text overHighScoreText;
 
    [SerializeField]
    Text overScoreText;

    [SerializeField]
    GameObject panel;

    public float maxsnow;
    private float minsnow;
    public int score;
    public int highScore;
    

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("ScoreUp", 1f, 1f);
        minsnow = 1 / maxsnow;
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void UpdateSlider(float value)
    {
        snowbar.value = value;
        if (snowbar.value >= 1)
        {
            snowbar.value = value = 1;
        }
        if (snowbar.value <= 0)
        {
            snowbar.value = value = 0;
        }

    }

    public void OnclickExitButton()
    {
        panel.SetActive(true);
    }

    public void OnclickYes()
    {
        Application.Quit();
    }
    public void OnclickNo()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

 

    //public void OpenObj(GameObject obj)
    //{
    //    obj.SetActive(true);
    //}
    //public void CloseObj(GameObject obj)
    //{
    //    obj.SetActive(false);
    //}
    public void OnclickOption()
    {
        Time.timeScale = 0;
    }

    public void OnclickResume()
    {
        Time.timeScale = 1;
    }


    void ScoreUp()
    {
        if(!SceneManager.Instance.isRunning)
            return;
      
        UpdateUI();
        score++;
        if (score > highScore)
        {
            highScore = score;
        }
    }
    void UpdateUI()
    {
        scoreText.text = string.Format($"SCORE {score}");
        highScoreText.text = string.Format($"HIGHSCORE {PlayerPrefs.GetInt("HIGHSCORE",0)}");
    }
    public void OverText()
    {
        overScoreText.text = string.Format($"SCORE {PlayerPrefs.GetInt("SCORE", 0)}");
        overHighScoreText.text = string.Format($"HIGHSCORE {PlayerPrefs.GetInt("HIGHSCORE", 0)}");
    }


}
