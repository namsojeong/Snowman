using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    Text scoreText = null;

    [SerializeField]
    Text highScoreText = null;

    private int score;
    private int highScore;

    private void Start()
    {
        InvokeRepeating("ScoreUp", 1f, 1f);
    }

    public void OpenObj(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void CloseObj(GameObject obj)
    {
        obj.SetActive(false);
    }
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
        score ++;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("SCORE", highScore);

        }
        highScore = PlayerPrefs.GetInt("SCORE", 0);
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = string.Format($"SCORE {score}");
        highScoreText.text = string.Format($"HIGHSCORE {highScore}");
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt( "HIGHSCORE", score);
        }
        UpdateUI();
    }


}
