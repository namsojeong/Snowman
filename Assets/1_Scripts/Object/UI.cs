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
        

    void Update()
    {
        score += 1;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("SCORE", score);
        }
        highScore = PlayerPrefs.GetInt("SCORE", 500);
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
