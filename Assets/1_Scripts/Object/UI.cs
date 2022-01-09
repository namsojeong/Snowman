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
    void Awake()
    {
        
    }


    private void Start()
    {
        InvokeRepeating("ScoreUp", 1f, 1f);
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
